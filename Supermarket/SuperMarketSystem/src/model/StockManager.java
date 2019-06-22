package model;


import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.util.ArrayList;


public class StockManager extends Employee {
	public void grantPermissions() {
		String username = this.getUsername();
		PreparedStatement P;
		try {
			P = con.prepareStatement("exec GrantStockManagerRole ? ");
			P.setString(1, username);
			P.execute();
			this.configureDetails();

		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

	@Override
	public void configureDetails(){
		this.setPosition("StockManager");
		}

	@Override
	public void createAccount(String username, String password) {
		PreparedStatement P;
		try {
			P = con.prepareStatement("exec CreateUserEmployee ?,?");
			P.setString(1, username);
			P.setString(2, password);
			P.execute();
			this.setUsername(username);
			this.setPassword(password);
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		
	}

	@Override
	public double getSalary() {
		
		return this.salary;
	}

	@Override
	public ArrayList<OrderTableEntry> getOrders() {
		Orders=new ArrayList<OrderTableEntry>();
		return Orders;
	}
}
