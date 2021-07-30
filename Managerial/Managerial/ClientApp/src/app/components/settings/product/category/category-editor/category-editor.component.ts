import { Component, OnInit, ViewChild } from '@angular/core';
import { AlertService, MessageSeverity } from 'src/app/services/notification/alert.service';
import { APiService } from 'src/app/services/generic/api.service';
import { Category } from 'src/app/models/product/Category.model';

@Component({
  selector: 'app-category-editor',
  templateUrl: './category-editor.component.html',
  styleUrls: ['./category-editor.component.scss']
})
export class CategoryEditorComponent {
  private isNewCategory = false;
  public isSaving: boolean;
  public showValidationErrors = true;
  public categoryEdit: Category = new Category();
  public selectedValues: { [key: string]: boolean; } = {};
  private editingCategoryName: string;

  public formResetToggle = true;

  public changesSavedCallback: () => void;
  public changesFailedCallback: () => void;
  public changesCancelledCallback: () => void;

  @ViewChild('f')
  private form;

  constructor(private alertService: AlertService, private categoryService: APiService) {
    this.categoryService.endPointUrl = 'categories';
  }

  showErrorAlert(caption: string, message: string) {
    this.alertService.showMessage(caption, message, MessageSeverity.error);
  }

  save() {
    this.isSaving = true;
    this.alertService.startLoadingMessage('Saving changes...');

    if (this.isNewCategory) {
      this.categoryService.post<Category>(this.categoryEdit).subscribe((category: Category) =>
        this.saveSuccessHelper(category), error => this.saveFailedHelper(error));
    } else {
      this.categoryService.put(this.categoryEdit).subscribe(() =>
        this.saveSuccessHelper(), error => this.saveFailedHelper(error));
    }
  }

  private saveSuccessHelper(category?: Category) {
    if (category) {
      Object.assign(this.categoryEdit, category);
    }

    this.isSaving = false;
    this.alertService.stopLoadingMessage();
    this.showValidationErrors = false;

    if (this.isNewCategory) {
      this.alertService.showMessage('Success', `category \"${this.categoryEdit.name}\" was created successfully`, MessageSeverity.success);
    } else {
      this.alertService.showMessage('Success', `Changes to category \"${this.categoryEdit.name}\" was saved successfully`, MessageSeverity.success);
    }

    this.categoryEdit = new Category();
    this.resetForm();

    if (this.changesSavedCallback) {
      this.changesSavedCallback();
    }
  }

  private saveFailedHelper(error: any) {
    this.isSaving = false;
    this.alertService.stopLoadingMessage();
    this.alertService.showStickyMessage('Save Error', 'The below errors occured whilst saving your changes:', MessageSeverity.error, error);
    this.alertService.showStickyMessage(error, null, MessageSeverity.error);

    if (this.changesFailedCallback) {
      this.changesFailedCallback();
    }
  }

  cancel() {
    this.categoryEdit = new Category();

    this.showValidationErrors = false;
    this.resetForm();

    this.alertService.showMessage('Cancelled', 'Operation cancelled by user', MessageSeverity.default);
    this.alertService.resetStickyMessage();

    if (this.changesCancelledCallback) {
      this.changesCancelledCallback();
    }
  }

  resetForm(replace = false) {
    if (!replace) {
      this.form.reset();
    } else {
      this.formResetToggle = false;

      setTimeout(() => {
        this.formResetToggle = true;
      });
    }
  }

  newCategory() {
    this.isNewCategory = true;
    this.showValidationErrors = true;

    this.editingCategoryName = null;
    this.selectedValues = {};
    this.categoryEdit = new Category();

    return this.categoryEdit;
  }

  editCategory(category: Category) {
    if (category) {
      this.isNewCategory = false;
      this.showValidationErrors = true;

      this.editingCategoryName = category.name;
      this.selectedValues = {};
      this.categoryEdit = new Category();
      Object.assign(this.categoryEdit, category);

      return this.categoryEdit;
    } else {
      return this.newCategory();
    }
  }
}
