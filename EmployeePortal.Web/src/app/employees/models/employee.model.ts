import { Department } from './department.model';

export interface Employee {
  uuid: string;
  department: Department;
  fullName: string;
  birthDate: string; // Формат 'YYYY-MM-DD'
  employmentDate: string; // Формат 'YYYY-MM-DD'
  salary: number;
}
