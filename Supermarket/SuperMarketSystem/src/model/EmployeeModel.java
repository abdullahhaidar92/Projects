package model;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;;


public class EmployeeModel {
	Connecting Connection;

	public EmployeeModel(Connecting connection) {
		Connection = connection;
		// connection.Connect("sa", "VoomBoom777Gm");
	}

	@SuppressWarnings("resource")
	public void AddEmployee(String EID, String FNAME, String LNAME, String BIRTHDATE, String ADDRESS, String PHONE,
			String GENDER, double SALARY, String SALARY_Currancy, String Position, boolean account, String Role,
			String user, String pass) throws SQLException {
		PreparedStatement P = Connection.con.prepareStatement("exec AddEmployee ?,?,?,?,?,?,?,?,?,?");
		P.setString(1, EID);
		P.setString(2, FNAME);
		P.setString(3, LNAME);
		P.setString(4, BIRTHDATE);
		P.setString(5, ADDRESS);
		P.setString(6, PHONE);
		P.setString(7, GENDER);
		P.setDouble(8, SALARY);
		P.setString(9, SALARY_Currancy);
		P.setString(10, Position);
		P.executeUpdate();
		if (account) {
			P = Connection.con.prepareStatement("exec CreateUserEmployee ?,?");
			P.setString(1, user);
			P.setString(2, pass);
			P.execute();
			if (Role.equals("Cashier"))
				P = Connection.con.prepareStatement("exec  GrantCashierRole ? ");
			else
				P = Connection.con.prepareStatement("exec GrantStockManagerRole ? ");
			P.setString(1, user);
			P.execute();

		}
	}

	public String[] getEmployee(String EID) {
		ResultSet rs;
		rs = Connection.getResults("Select * from EMPLOYEE where EID='" + EID + "'  ");
		try {
			if (rs.next()) {
				String[] s = new String[10];
				s[0] = rs.getString("EID");
				s[1] = rs.getString("FNAME");
				s[2] = rs.getString("LNAME");
				s[3] = rs.getDate("BIRTHDATE").toString();
				s[4] = rs.getString("ADDRESS");
				s[5] = rs.getString("PHONE");
				s[6] = rs.getString("GENDER");
				s[7] = "" + rs.getDouble("SALARY");
				s[8] = rs.getString("SALARY_Currancy");
				s[9] = rs.getString("Position");
				return s;
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return null;

	}

	public String[] getStats() throws SQLException {
		int c = 0, s = 0;
		String[] result = new String[] { "text1", "text1", "text1", "text1", "text1", "text1" };
		// Call the Manager
		Manager manager = Manager.getManager();
		manager.setCon(Connection.con);
		manager.setSarlary_Currancy("LLP");
		ResultSet rs = null;
		rs = Connection.getResults("Select * from Employee");
		EmployeeFactory factory = new EmployeeFactory();
		while (rs.next()) {
			String pos = rs.getString("Position").trim();
			if (pos.equals("MASTER")||pos.equals("Administrater"))
				continue;
			Employee emp = factory.createEmployee(pos);
			emp.setId(rs.getString("EID"));
			emp.setSalary(rs.getDouble("Salary"));
			manager.getEmployees().add(emp);
			if (pos.equals("Cashier"))
				c++;
			if (pos.equals("StockManager"))
				s++;

		}

		result[0] = "" + manager.getEmployees().size();
		result[1] = "" + c;
		result[2] = "" + s;
		result[3]=""+manager.getSalary()+"  "+manager.getSarlary_Currancy();
		result[4] = "" + manager.getOrders().size();
		result[5]=""+(int)manager.getSum()+ "  "+manager.getSarlary_Currancy();
		return result;
	}
	
	
	public ArrayList<String> getResults(String visitor){
		ArrayList<String> results=new ArrayList<String>();
		if(visitor.equals("StatisticsRevenue")) {
		RevenueStats rv=new RevenueStats();
		Manager m=Manager.getManager();
	    m.accept(rv);
	    results=rv.getResult();
		}
		return results;
		
	}

}
