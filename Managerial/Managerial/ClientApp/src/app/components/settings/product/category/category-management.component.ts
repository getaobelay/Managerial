import { Component, OnInit, AfterViewInit, TemplateRef, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AlertService, MessageSeverity, DialogType } from 'src/app/services/notification/alert.service';
import { Utilities } from 'src/app/services/app/utilities';
import { AppTranslationService } from 'src/app/services/app/app-translation.service';
import { Category } from 'src/app/models/product/Category.model';
import { CategoryEditorComponent } from './category-editor/category-editor.component';
import { ProductService } from 'src/app/components/products/product.service';

@Component({
  selector: 'app-category-management',
  templateUrl: './category-management.component.html',
  styleUrls: ['./category-management.component.scss']
})
export class CategoryManagementComponent implements OnInit, AfterViewInit {
  columns: any[] = [];
  rows: Category[] = [];
  rowsCache: Category[] = [];
  editedCategory: Category;
  sourceCategory: Category;
  editingCategoryName: { name: string };
  loadingIndicator: boolean;

  @ViewChild('indexTemplate', { static: true })
  indexTemplate: TemplateRef<any>;

  @ViewChild('name', { static: true })
  NameTemplate: TemplateRef<any>;

  @ViewChild('description', { static: true })
  DescriptionTemplate: TemplateRef<any>;

  @ViewChild('actionsTemplate', { static: true })
  actionsTemplate: TemplateRef<any>;

  @ViewChild('editorModal', { static: true })
  editorModal: ModalDirective;

  @ViewChild('categoryEditor', { static: true })
  CategoryEditor: CategoryEditorComponent;

  constructor(private alertService: AlertService,
     private translationService: AppTranslationService,
     private categoryService: ProductService) {
  }

  ngOnInit() {
    const gT = (key: string) => this.translationService.getTranslation(key);

    this.columns = [
      { prop: 'name', name: 'name', width: 50, cellTemplate: this.NameTemplate },
      { prop: 'description', name: 'description', width: 90, cellTemplate: this.DescriptionTemplate },
    ];

    this.columns.push({ name: '', width: 160, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false });

    this.loadData();
  }

  ngAfterViewInit() {
    this.CategoryEditor.changesSavedCallback = () => {
      this.AddNewCategory();
      this.editorModal.hide();
    };

    this.CategoryEditor.changesCancelledCallback = () => {
      this.editedCategory = null;
      this.sourceCategory = null;
      this.editorModal.hide();
    };
  }

  AddNewCategory() {
    if (this.sourceCategory) {
      Object.assign(this.sourceCategory, this.editedCategory);

      let sourceIndex = this.rowsCache.indexOf(this.sourceCategory, 0);
      if (sourceIndex > -1) {
        Utilities.moveArrayItem(this.rowsCache, sourceIndex, 0);
      }

      sourceIndex = this.rows.indexOf(this.editedCategory, 0);
      if (sourceIndex > -1) {
        Utilities.moveArrayItem(this.rows, sourceIndex, 0);
      }

      this.editedCategory = null;
      this.editedCategory = null;
    } else {
      const category = new Category();
      Object.assign(category, this.editedCategory);
      this.editedCategory = null;

      let maxIndex = 0;
      for (const r of this.rowsCache) {
        if ((r as any).index > maxIndex) {
          maxIndex = (r as any).index;
        }
      }

      (category as any).index = maxIndex + 1;

      this.rowsCache.splice(0, 0, category);
      this.rows.splice(0, 0, category);
      this.rows = [...this.rows];
    }
  }

  loadData() {
    this.alertService.startLoadingMessage();
    this.loadingIndicator = true;

    this.categoryService.categoryService.getAll<Category>()
      .subscribe(results => {
        this.alertService.stopLoadingMessage();
        this.loadingIndicator = false;

        const Categorys = results;

        this.rowsCache = [...Categorys];
        this.rows = Categorys;
      },
        error => {
          this.alertService.stopLoadingMessage();
          this.loadingIndicator = false;

          this.alertService.showStickyMessage('Load Error', `Unable to retrieve roles from the server.\r\nErrors: "${Utilities.getHttpResponseMessages(error)}"`,
            MessageSeverity.error, error);
        });
  }

  onSearchChanged(value: string) {
    this.rows = this.rowsCache.filter(r => Utilities.searchArray(value, false, r.name, r.description));
  }

  onEditorModalHidden() {
    this.editingCategoryName = null;
    this.CategoryEditor.resetForm(true);
  }

  newCategory() {
    this.editingCategoryName = null;
    this.sourceCategory = null;
    this.editedCategory = this.CategoryEditor.newCategory();
    this.editorModal.show();
  }

  editCategory(row: Category) {
    this.editingCategoryName = { name: row.name };
    this.sourceCategory = row;
    this.editedCategory = this.CategoryEditor.editCategory(row);
    this.editorModal.show();
  }

  deleteCategory(row: Category) {
    this.alertService.showDialog('Are you sure you want to delete the \"' + row.name + '\" Category?',
      DialogType.confirm, () => this.deleteCategoryHelper(row));
  }
a
  deleteCategoryHelper(row: Category) {
    this.alertService.startLoadingMessage('Deleting...');
    this.loadingIndicator = true;

    this.categoryService.categoryService.delete<Category>(row.id)
      .subscribe(results => {
        this.alertService.stopLoadingMessage();
        this.loadingIndicator = false;

        this.rowsCache = this.rowsCache.filter(item => item !== row);
        this.rows = this.rows.filter(item => item !== row);
      },
        error => {
          this.alertService.stopLoadingMessage();
          this.loadingIndicator = false;

          this.alertService.showStickyMessage('Delete Error', `An error occured whilst deleting the deleteCategory.\r\nError: "${Utilities.getHttpResponseMessages(error)}"`,
            MessageSeverity.error, error);
        });
  }
}
