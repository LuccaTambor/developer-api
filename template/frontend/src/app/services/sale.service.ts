import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SaleService {
  private BASE_URL = '/api/sales';

  async getPaged() {
    const url = `${this.BASE_URL}?_page=1&_size=10`;
    const result = await fetch(url)
    return result.json();
  }

  constructor() { }
}
