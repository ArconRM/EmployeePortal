import { Component, computed, inject, Input, OnInit, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { NgbActiveModal, NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { EmployeeService } from '../employees/services/employee.service';
import { Employee } from '../employees/models/employee.model';
import { Department } from '../employees/models/department.model';

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
    birthDate: ['', Validators.required],
    employmentDate: ['', Validators.required],
    salary: [0, [Validators.required, Validators.min(0)]]
  });

  ngOnInit(): void {
    this.#employeeService.getDepartments().subscribe(deps => this.departments.set(deps));

    if (this.isEditMode() && this.employee) {
      this.form.patchValue({
        ...this.employee,
        departmentUuid: this.employee.department.uuid
      });
    }
  }

  onSave(): void {
    if (this.form.invalid) return;

    const payload = this.form.getRawValue();
    const request$ = this.isEditMode()
      ? this.#employeeService.updateEmployee(payload)
      : this.#employeeService.createEmployee(payload);

    request$.subscribe(() => this.activeModal.close(true));
  }
}
