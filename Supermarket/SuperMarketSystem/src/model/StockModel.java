package model;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

import javafx.collections.FXCollections;
import javafx.collections.ObservableList;

public class StockModel {
	Connecting Connection;
	public ObservableList<StockEntry> data;
	public String[] SupplierInfo = new String[8];

	public StockModel(Connecting connection) {
		Connection = connection;
		//connection.Connect("sa", "VoomBoom777Gm");
		data = FXCollections.observableArrayList();
	}

	public void getStocks() {
		data.removeAll(data);
		ResultSet rs;
		rs = Connection.getResults(
				"Select * from Stock join PRODUCT on Stock.PID=PRODUCT.PID join SUPPLIER_PRODUCT on STOCK.PID=SUPPLIER_PRODUCT.PID");
		try {
			while (rs.next()) {
				data.add(new StockEntry(rs.getString("SID"), rs.getString("PNAME"),
						rs.getInt("MIN"), rs.getInt("MAX"), rs.getInt("Quantity"), rs.getString("SUPPID")));

			}

		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

	public void getEmptyStocks() {
		data.removeAll(data);
		ResultSet rs;
		rs = Connection.getResults(
				"Select * from Stock join PRODUCT on Stock.PID=PRODUCT.PID join SUPPLIER_PRODUCT on STOCK.PID=SUPPLIER_PRODUCT.PID Where Stock.Quantity <= Stock.MIN");
		try {
			while (rs.next()) {
				data.add(new StockEntry(rs.getString("SID"), rs.getString("PNAME"), 
						rs.getInt("MIN"), rs.getInt("MAX"), rs.getInt("Quantity"), rs.getString("SUPPID")));

			}

		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

	public String[] getSupplierInfo(String SupplierId) throws SQLException {
		ResultSet rs = Connection.getResults("select * from SUPPLIER where SUPPID='" + SupplierId + "'  ");
	//	System.out.println("select * from SUPPLIER where SUPPID='" + SupplierId + "'  ");
		if (rs.next()) {
			SupplierInfo[0] = rs.getString("SUPPID");
			SupplierInfo[1] = rs.getString("FNAME");
			SupplierInfo[2] = rs.getString("LNAME");
			SupplierInfo[3] = rs.getString("ADDRESS");
			SupplierInfo[4] = rs.getString("PHONE");
			SupplierInfo[5] = rs.getString("GENDER");
			SupplierInfo[6] = rs.getString("COMPANY");
			return SupplierInfo;

		}
		return null;

	}

	public void AddStock(String SID, String PID, int Q, int m, int M) throws SQLException {
		PreparedStatement P = Connection.con.prepareStatement("exec AddStock  ?,?,?,?,?");
		P.setString(1, SID);
		P.setString(2, PID);
		P.setInt(3, Q);
		P.setInt(4, m);
		P.setInt(5, M);

		P.executeUpdate();

	}

	public void AddToStock(String SID, int Q) throws SQLException {
		PreparedStatement P = Connection.con.prepareStatement("exec AddtoStock  ?,?");
		P.setString(1, SID);
		P.setInt(2, Q);
		P.executeUpdate();

	}

	public StockEntry getStock(String SID) {
		ResultSet rs;
		rs = Connection.getResults("Select * from Stock join PRODUCT on Stock.PID=PRODUCT.PID  where SID='" + SID + "'  ");
		try {
			if (rs.next()) {
				return (new StockEntry(rs.getString("SID"), rs.getString("PNAME"),rs.getInt("MIN"), rs.getInt("MAX"),
						rs.getInt("Quantity"), ""));
			}

		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return null;

	}

}
