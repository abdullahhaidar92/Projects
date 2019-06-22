package model;

public class EmployeeFactory {
	Employee employee;

	public Employee createEmployee(String type) {
		//System.out.println(type);
		//System.out.println(type.length());
		if (type.equals("Cashier")) {
			//System.out.println("Creating new Cashier");
			return new Cashier();
		}
		if (type.equals("StockManager")) {
			///System.out.println("Creating new StockManager");
			return new StockManager();
		}
		if (type.equals("MASTER")||type.equals("Administrater")) {
			//System.out.println("Creating Manager");
			return Manager.getManager();
		}
		System.out.println("Invalid");
		return null;
	}
}
