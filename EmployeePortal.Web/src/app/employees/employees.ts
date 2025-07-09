import { Component, ChangeDetectionStrategy, computed, effect, inject, signal } from '@angular/core';
import { DatePipe, CurrencyPipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgbDatepickerModule, NgbModal, NgbPagination } from '@ng-bootstrap/ng-bootstrap';
import { EmployeeService } from './services/employee.service';
import { Employee } from './models/employee.model';
import { EmployeeDialogComponent } from '../components/employee-dialog';
import { ConfirmDialogComponent } from '../components/confirm-dialog';

@Component({
  selector: 'app-employees',
  standalone: true,
  imports: [
    FormsModule,
    DatePipe,
    CurrencyPipe,
    NgbDatepickerModule,
    NgbPagination
  ],
  providers: [DatePipe, CurrencyPipe],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './employees.html',
  // styleUrl: './employees.component.css'
})
export class EmployeesComponent {
  #employeeService = inject(EmployeeService);
  #modalService = inject(NgbModal);
  #datePipe = inject(DatePipe);

  // Сигналы для состояния
  employees = signal<Employee[]>([]);
  totalCount = signal(0);
  loading = signal(true);

  // Сигналы для фильтров
  fullNameFilter = signal('');
  departmentFilter = signal('');
  birthDateFilter = signal<Date | null>(null);
  employmentDateFilter = signal<Date | null>(null);
  salaryFilter = signal('');

  // Сигналы для сортировки
  sortColumn = signal<string | null>(null);
  sortDirection = signal<'asc' | 'desc'>('asc');

  // Сигналы для пагинации
  pageNumber = signal(1);
  pageSize = signal(10);

  displayedColumns = ['department', 'fullName', 'birthDate', 'employmentDate', 'salary', 'actions'];

  constructor() {
    effect(() => {
      this.loadEmployees();
    }, {allowSignalWrites: true});
  }

  handleSort(column: string) {
    if (this.sortColumn() === column) {
      this.sortDirection.update(dir => dir === 'asc' ? 'desc' : 'asc');
    } else {
      this.sortColumn.set(column);
      this.sortDirection.set('asc');
    }
  }

  openCreateDialog(): void {
    const modalRef = this.#modalService.open(EmployeeDialogComponent, {size: 'lg'});
    modalRef.result.then((result) => {
      if (result) {
        this.loadEmployees();
      }
    }).catch(() => {
    });
  }

  openEditDialog(employee: Employee): void {
    const modalRef = this.#modalService.open(EmployeeDialogComponent, {size: 'lg'});
    modalRef.componentInstance.employee = employee;

    modalRef.result.then((result) => {
      if (result) {
        this.loadEmployees();
      }
    }).catch(() => {
    });
  }

  openDeleteDialog(employee: Employee): void {
    const modalRef = this.#modalService.open(ConfirmDialogComponent);
    modalRef.componentInstance.title = 'Подтверждение удаления';
    modalRef.componentInstance.message = `Вы уверены, что хотите удалить сотрудника ${employee.fullName}?`;

    modalRef.result.then((confirmed) => {
      if (confirmed) {
        this.#employeeService.deleteEmployee(employee.uuid).subscribe(() => {
          this.loadEmployees();
        });
      }
    }).catch(() => {
    });
  }

  loadEmployees() {
    this.loading.set(true);

    const filters = {
      fullName: this.fullNameFilter(),
      department: this.departmentFilter(),
      birthDate: this.birthDateFilter() ? this.#datePipe.transform(this.birthDateFilter(), 'yyyy-MM-dd')! : '',
      employmentDate: this.employmentDateFilter() ? this.#datePipe.transform(this.employmentDateFilter(), 'yyyy-MM-dd')! : '',
      salary: this.salaryFilter()
    };
    const sort = {column: this.sortColumn(), direction: this.sortDirection()};

    this.#employeeService.getEmployees(this.pageNumber(), this.pageSize(), filters, sort)
      .subscribe(response => {
        this.employees.set(response.items);
        this.totalCount.set(response.totalCount);
        this.loading.set(false);
      });
  }
}
