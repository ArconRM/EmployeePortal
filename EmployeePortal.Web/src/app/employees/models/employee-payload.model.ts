export interface EmployeeCreatePayload {
  departmentUuid: string;
  fullName: string;
  birthDate: string;
  employmentDate: string;
  salary: number;
}

export interface EmployeeUpdatePayload extends EmployeeCreatePayload {
  uuid: string;
}
