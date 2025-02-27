import { Component, inject } from '@angular/core';
import { SaleService } from '../services/sale.service';
import { Sale } from '../models/Sale';
import { TableModule } from 'primeng/table';
import { MenubarModule } from 'primeng/menubar';

@Component({
  selector: 'app-home',
  imports: [TableModule, MenubarModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.less'
})
export class HomeComponent {
  private saleService = inject(SaleService);
  sales: Sale[] = [];

  getSales = async () => {
    await this.saleService.getPaged()
      .then((result) => {
        this.sales = result.data.data;
      })
  }

  items = [
    {
      label: 'Sales',
      onclick: this.getSales()
    },
    {
      label: 'Products',
    },
  ]
}
