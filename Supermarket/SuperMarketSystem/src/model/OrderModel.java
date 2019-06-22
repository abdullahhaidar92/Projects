package model;


import java.sql.ResultSet;
import java.sql.SQLException;

import javafx.collections.FXCollections;
import javafx.collections.ObservableList;

public class OrderModel {
	Connecting Connection;
	public ObservableList<OrderTableEntry> data;
	public ObservableList<OrderEntry> data1;

	public OrderModel(Connecting connection) {
		Connection = connection;
		//connection.Connect("sa", "VoomBoom777Gm");
		data = FXCollections.observableArrayList();
		data1 = FXCollections.observableArrayList();
	}

	public void getOrders() {
		data.removeAll(data);
		ResultSet rs;
		rs = Connection.getResults("Select * from OrdersView Order by OID Desc");
		try {
			while (rs.next()) {
				data.add(new OrderTableEntry(rs.getInt("OID"), rs.getString("CID"),
						rs.getString("CusFName").trim() + " " + rs.getString("CusLName"), rs.getString("EID"),
						rs.getString("EmpFName").trim() + " " + rs.getString("EmpLName"), rs.getDouble("Total"),
						rs.getString("TOTAL_Currancy"), rs.getDate("Date").toString()));

			}

		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		
	}
	
	
	public void getEmployeeOrders(String EID) {
		data.removeAll(data);
		ResultSet rs;
		rs = Connection.getResults("Select * from OrdersView where EID='"+EID+"'    Order by OID Desc");
		try {
			while (rs.next()) {
				data.add(new OrderTableEntry(rs.getInt("OID"), rs.getString("CID"),
						rs.getString("CusFName").trim() + " " + rs.getString("CusLName"), rs.getString("EID"),
						rs.getString("EmpFName").trim() + " " + rs.getString("EmpLName"), rs.getDouble("Total"),
						rs.getString("TOTAL_Currancy"), rs.getDate("Date").toString()));

			}

		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		
	}
	
	public void getDateOrders(String Date1,String Date2) throws SQLException {
		data.removeAll(data);
		ResultSet rs;
	
		rs = Connection.getResults("Select * from OrdersView where Date between '"+Date1+"'  and '"+Date2+"'    Order by OID Desc");
	    rs.next() ;
		try {
			while (rs.next()) {
				
				data.add(new OrderTableEntry(rs.getInt("OID"), rs.getString("CID"),
						rs.getString("CusFName").trim() + " " + rs.getString("CusLName"), rs.getString("EID"),
						rs.getString("EmpFName").trim() + " " + rs.getString("EmpLName"), rs.getDouble("Total"),
						rs.getString("TOTAL_Currancy"), rs.getDate("Date").toString()));

			}

		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		
	}
	
	
	
	public ResultSet getOrderDetails(String OID) {
		ResultSet rs;
		rs = Connection.getResults("Select * from Order_Products where OID="+OID);
		return rs;
	}
	
	
	public ResultSet getOrderInfo(String OID) {

		ResultSet rs;
		rs = Connection.getResults("Select * from [Order] where OID="+OID);
		try {
			rs.next();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return rs;
	}
}
