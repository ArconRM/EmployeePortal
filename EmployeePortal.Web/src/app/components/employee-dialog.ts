import { Component, computed, inject, Input, OnInit, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { NgbActiveModal, NgbDatepickerModule, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { EmployeeService } from '../employees/services/employee.service';
import { Employee } from '../employees/models/employee.model';
import { Department } from '../employees/models/department.model';
import { EmployeeUpdatePayload } from '../employees/models/employee-payload.model';

@Component({
  standalone: true,
  imports: [ReactiveFormsModule, NgbDatepickerModule],
  template: `
    <div class="modal-header">
      <h4 class="modal-title">{{ isEditMode() ? 'Редактировать сотрудника' : 'Добавить сотрудника' }}</h4>
      <button type="button" class="btn-close" (click)="activeModal.dismiss()"></button>
    </div>
    <div class="modal-body" [formGroup]="form">
      <div class="mb-3">
        <label for="fullName" class="form-label">Ф.И.О.</label>
        <input type="text" id="fullName" class="form-control" formControlName="fullName">
      </div>
      <div class="mb-3">
        <label for="department" class="form-label">Отдел</label>
        <select id="department" class="form-select" formControlName="departmentUuid">
          @for (dept of departments(); track dept.uuid) {
            <option [value]="dept.uuid">{{ dept.name }}</option>
          }
        </select>
      </div>
      <div class="mb-3">
        <label for="birthDate" class="form-label">Дата рождения</label>
        <div class="input-group">
          <input
            id="birthDate"
            class="form-control"
            placeholder="yyyy-mm-dd"
            name="dpBirthDate"
            formControlName="birthDate"
            ngbDatepicker
            #d1="ngbDatepicker"
          />
          <button class="btn btn-outline-secondary" (click)="d1.toggle()" type="button">📅</button>
        </div>
      </div>
      <div class="mb-3">
        <label for="employmentDate" class="form-label">Дата приёма</label>
        <div class="input-group">
          <input
            id="employmentDate"
            class="form-control"
            placeholder="yyyy-mm-dd"
            name="dpEmploymentDate"
            formControlName="employmentDate"
            ngbDatepicker
            #d2="ngbDatepicker"
          />
          <button class="btn btn-outline-secondary" (click)="d2.toggle()" type="button">📅</button>
        </div>
      </div>
      <div class="mb-3">
        <label for="salary" class="form-label">Оклад</label>
        <input type="number" id="salary" class="form-control" formControlName="salary">
      </div>
    </div>
    <div class="modal-footer">
      <button type="button" class="btn btn-outline-secondary" (click)="activeModal.dismiss()">Отмена</button>
      <button type="button" class="btn btn-primary" [disabled]="form.invalid" (click)="onSave()">Сохранить</button>
    </div>
  `
})
export class EmployeeDialogComponent implements OnInit {
  activeModal = inject(NgbActiveModal);
  #fb = inject(FormBuilder);
  #employeeService = inject(EmployeeService);

  @Input() employee: Employee | null = null;

  isEditMode = computed(() => !!this.employee);
  departments = signal<Department[]>([]);

  form = this.#fb.nonNullable.group({
    fullName: ['', Validators.required],
    departmentUuid: ['', Validators.required],
    birthDate: [null as NgbDateStruct | null, Validators.required],
    employmentDate: [null as NgbDateStruct | null, Validators.required],
    salary: [0, [Validators.required, Validators.min(0)]]
  });

  ngOnInit(): void {
    this.#employeeService.getDepartments().subscribe(deps => this.departments.set(deps));

    if (this.isEditMode() && this.employee) {
      this.form.patchValue({
        fullName: this.employee.fullName,
        departmentUuid: this.employee.department.uuid,
        salary: this.employee.salary,
        birthDate: this.parseDateToNgbDate(this.employee.birthDate),
        employmentDate: this.parseDateToNgbDate(this.employee.employmentDate)
      });
    }
  }

  onSave(): void {
    if (this.form.invalid) return;

    const formValue = this.form.getRawValue();
    const payload = {
      ...formValue,
      birthDate: this.formatNgbDateToString(formValue.birthDate!),
      employmentDate: this.formatNgbDateToString(formValue.employmentDate!)
    };

    if (this.isEditMode() && this.employee) {
      const updatePayload: EmployeeUpdatePayload = {
        ...payload,
        uuid: this.employee.uuid
      };
      this.#employeeService.updateEmployee(updatePayload)
        .subscribe(() => this.activeModal.close(true));
    } else {
      this.#employeeService.createEmployee(payload)
        .subscribe(() => this.activeModal.close(true));
    }
  }

  private parseDateToNgbDate(dateString: string): NgbDateStruct | null {
    if (!dateString) return null;
    const date = new Date(dateString);
    return {
      year: date.getFullYear(),
      month: date.getMonth() + 1,
      day: date.getDate()
    };
  }

  private formatNgbDateToString(date: NgbDateStruct): string {
    return `${date.year}-${this.padNumber(date.month)}-${this.padNumber(date.day)}`;
  }

  private padNumber(num: number): string {
    return num.toString().padStart(2, '0');
  }
}
