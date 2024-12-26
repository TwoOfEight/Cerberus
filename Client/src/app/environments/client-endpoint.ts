export class ClientEndpoint {
  static home: string = '';

  /** ERRORS
   **************************************/
  static pageNotFoundError: string = `${this.home}/404`;
  static unauthorizedAccessError: string = `${this.home}/401`;

  /** AUTH
   **************************************/
  static register: string = `${this.home}/user/register`;
  static login: string = `${this.home}/user/login`;

  // static profile: string = `${this.frontend}/user/profile`; // If needed later

  /** FEATURES
   **************************************/
  static employeeAdd: string = `${this.home}/employee/add`;
  static employeeList: string = `${this.home}/employee/list`;

  // static updateTurbine: string = `${this.frontend}/turbines/${id}`; // If needed later
}
