package model;

import java.sql.*;

public class Connecting {
	private  ResultSet rs = null;
	private  PreparedStatement P;
	private String server = "localhost";
	private int port = 1433;
	private String user ;
	private String password ;
	private String database = "SuperMarket";
	private String jdbcurl;
	public  Connection con = null;

	
	public boolean Connect(String u,String p) {
		user=u;
		password=p;
		try {
			Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");

		} catch (ClassNotFoundException e) {
			e.printStackTrace();
		}
		jdbcurl = "jdbc:sqlserver://" + server + ":" + port + ";user=" + user + ";password=" + password
				+ ";databasename=" + database + "";
		try {

			con = DriverManager.getConnection(jdbcurl);
			return true;
			//System.out.println("Worked");

		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
return false;
	}

	
public ResultSet getResults(String sql) {

		try {
			P = con.prepareStatement(sql);
			rs = P.executeQuery();

		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return rs;

	}

public void execute(String sql) {

	try {
		P = con.prepareStatement(sql);
		P.executeUpdate();

	} catch (SQLException e) {
		// TODO Auto-generated catch block
		e.printStackTrace();
	}


}

}