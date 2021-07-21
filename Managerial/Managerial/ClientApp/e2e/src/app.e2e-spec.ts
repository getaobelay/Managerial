// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

import { AppPage } from './app.po';

describe('Managerial App', () => {
  let page: AppPage;

  beforeEach(() => {
    page = new AppPage();
  });

  it('should display application title: Managerial', async () => {
    await page.navigateTo();
    expect(await page.getAppTitle()).toEqual('Managerial');
  });
});
