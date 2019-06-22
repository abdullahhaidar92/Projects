package model;


import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.util.ArrayList;


import model.OrderTableEntry;

public class Manager extends Employee implements StatisticsVisitable {
	public static Manager manager;
	ArrayList<Employee> employees;
	
	public double getSum() {
		return this.sum;
	}
	public void setSum(double sum) {
		this.sum = sum;
	}
	private Manager() {
		this.configureDetails();
		employees=new ArrayList<Employee>();
	}
	public static Manager getManager() {
		if(manager==null) {
			manager=new Manager();
			//System.out.println("Manager Created");
		}
		else {
			//System.out.println("Manger already exists");
		}
		return manager;
		
	}
	public void grantPermissions() {
		String username = this.getUsername();
		PreparedStatement P;
		try {
			P = con.prepareStatement(" Grant all privileges to  "+this.getUsername());
			P.setString(1, username);
			P.execute();

		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}
	@Override
	public void configureDetails()  {
	this.setPosition("MASTER");
	this.setUsername("sa");
	this.setId("dbo");
		
	}
	@Override
	public void createAccount(String username, String password) {
		this.setUsername("sa");
		this.setPassword(password);
		System.out.println("The manger has the sa account");
		
	}
	@Override
	public double getSalary() {
		double s=0;
		for(int i=0;i<employees.size();i++)
			s+=employees.get(i).getSalary();
		return s;
	}
	@Override
	public ArrayList<OrderTableEntry> getOrders() {
		sum=0;
		ArrayList<OrderTableEntry> Orders=new ArrayList<OrderTableEntry>();
		for(int i=0;i<employees.size();i++){
			employees.get(i).setCon(con);
			Orders.addAll(employees.get(i).getOrders());
			sum+=employees.get(i).sum;
		}
		return Orders;
	}
	public ArrayList<Employee> getEmployees() {
		return employees;
	}
	public void setEmployees(ArrayList<Employee> employees) {
		this.employees = employees;
	}
	@Override
	public void accept(StatisticsVisitor v) {
		v.visit(this);
		
	}
}
