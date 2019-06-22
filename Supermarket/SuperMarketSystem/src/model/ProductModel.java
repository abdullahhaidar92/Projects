package model;

import java.sql.PreparedStatement;

import java.sql.SQLException;


public class ProductModel {
	Connecting Connection;

	public ProductModel(Connecting connection) {
		Connection = connection;
		// connection.Connect("sa", "VoomBoom777Gm");

	}
	public void addProduct(String PID,String PName,String Category,double Price,String Currancy,String Date,
			double TVA,String Barcode,String image) throws SQLException {
		
		
	PreparedStatement P=Connection.con.prepareStatement("exec AddProduct ?,?,?,?,?,?,?,?,?");
		P.setString(1,PID);
		P.setString(2,PName);
		P.setString(3,Category);
		P.setDouble(4,Price);
		P.setString(5,Currancy);
		P.setString(6,Date);
		P.setDouble(7,TVA);
		P.setString(8,Barcode);
		P.setString(9,image);
		P.executeUpdate();
	
	}

}
