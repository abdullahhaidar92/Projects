package model;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;



public class Cashier extends Employee {
	public void grantPermissions() {
		String username = this.getUsername();
		PreparedStatement P;
		try {
			P = con.prepareStatement("exec GrantCashierRole ? ");
			P.setString(1, username);
			P.execute();

		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

	@Override
	public void configureDetails() {
		this.setPosition("Cashier");

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
		Orders = new ArrayList<OrderTableEntry>();
		sum=0;
		ResultSet rs = null;
		try {
			rs = (con.prepareStatement("Select * from OrdersView where EID='" + this.getId() + "'"))
					.executeQuery();
		} catch (SQLException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		}
		try {
			while (rs.next()) {
				
				Orders.add(new OrderTableEntry(rs.getInt("OID"), rs.getString("CID"),
						rs.getString("CusFName").trim() + " " + rs.getString("CusLName"), rs.getString("EID"),
						rs.getString("EmpFName").trim() + " " + rs.getString("EmpLName"), rs.getDouble("Total"),
						rs.getString("TOTAL_Currancy"), rs.getDate("Date").toString()));
			   sum+= rs.getDouble("Total");
			}

		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
     return Orders;
	}
}
