import { Page } from "./page";

export interface EmployeeCreate {
    name: string;
    rank: string;
}

export interface Employee {
    id: string;
    name: string;
    rank: string;
}

export interface EmployeePage extends Page<Employee> {}