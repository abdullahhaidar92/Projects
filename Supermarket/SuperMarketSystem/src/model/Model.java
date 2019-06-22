package model;


import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;


import javafx.collections.FXCollections;
import javafx.collections.ObservableList;


public class Model {
	private Connecting Connection;
	protected String Type, id;
	public final ObservableList<OrderEntry> data;
	private ResultSet rs;
	protected String[][] ImageArray;
	private float tva;

	public Model() {
		Connection = new Connecting();
		data = FXCollections.observableArrayList();
		/*
		 * data = FXCollections.observableArrayList(new OrderEntry("", "Milk", "Nido",
		 * 4, "10 %", 7500, "LLP"), new OrderEntry("", "Milk", "Nido", 3, "10 %", 7500,
		 * "LLP"), new OrderEntry("", "Milk", "Nido", 2, "10 %", 7500, "LLP"), new
		 * OrderEntry("", "Milk", "Nido", 1, "10 %", 7500, "LLP"));
		 */
	}

	public boolean VerifyConnection(String id, String pass) {
		boolean worked = true;
		this.id = id;
		try {
			if (Connection.Connect(id, pass)) {
				if (!id.equals("sa"))
					Type = getType();
				else {
					Type = "MASTER";

				}
			}
		} catch (Exception e) {
			e.printStackTrace();
			worked = false;
		}

		return worked;
	}

	public double addToTable(String barCode, int Quantity) throws SQLException {
		ResultSet rs = Connection.getResults("Select * from PRODUCT where BARCODE='" + barCode + "' ");

		if (rs.next()) {
			data.add(new OrderEntry(rs.getString("PID"), rs.getString("PNAME").trim(), Quantity,
					(int) (rs.getDouble("TVA") * 100) + "%", rs.getDouble("PRICE"),
					rs.getString("PRICE_Currancy").trim()));
			tva += rs.getDouble("PRICE") * rs.getDouble("TVA") * Quantity;
			return (rs.getDouble("PRICE") * (1 + rs.getDouble("TVA")) * Quantity);

		}
		return 0;
	}

	public double addImageToTable(String PID, int Quantity) throws SQLException {
		ResultSet rs = Connection.getResults("Select * from PRODUCT where PID='" + PID + "' ");

		if (rs.next()) {
			data.add(new OrderEntry(rs.getString("PID").trim(), rs.getString("PNAME").trim(), Quantity,
					(rs.getDouble("TVA") * 100) + "%", rs.getDouble("PRICE"), rs.getString("PRICE_Currancy").trim()));
			tva += rs.getDouble("PRICE") * rs.getDouble("TVA") * Quantity;
			return (rs.getDouble("PRICE") * (1 + rs.getDouble("TVA")) * Quantity);

		}
		return 0;
	}

	public int GenerateBill(String CID) throws SQLException {
		int OID = 0;
		ResultSet rs = Connection.getResults("Select max(OID)  as maxOID from [ORDER]");
		PreparedStatement P = Connection.con.prepareStatement("exec AddOrder ?,?");
		if (rs.next()) {
			OID = rs.getInt("maxOID") + 1;
			P.setInt(1, OID);
			P.setString(2, CID);
			P.executeUpdate();
			P = Connection.con.prepareStatement("exec AddItemToOrder ?,?,?");
			for (int i = 0; i < data.size(); i++) {
				P.setInt(1, OID);
				P.setString(2, data.get(i).getPid());
				P.setInt(3, data.get(i).getQuantity());
				P.executeUpdate();

			}
		}
		return OID;
	}

	public ResultSet getBill(int OID) {

		ResultSet rs = Connection.getResults("Select * from OrdersView where OID=" + OID);
		return rs;
	}

	public String getType() {
			if(!(Type==null)&&((Type.equals("MASTER")||this.Type.equals("Administrater"))))
			return "MASTER";
		rs = Connection.getResults("Select Position from ViewEmployeeInfo" + id);
		try {
			if (rs.next())
				return rs.getString("Position");
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return null;
	}

	public String getFullName() {
		rs = Connection.getResults("Select FNAME,LNAME from ViewEmployeeInfo" + id);
		try {
			if (rs.next())
				return (rs.getString("FNAME") + " " + rs.getString("LNAME"));
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return null;
	}

	public int fillImageArray() throws SQLException {
		ImageArray = new String[20][2];
		ResultSet rs = Connection
				.getResults("Select PID,IMAGE  from PRODUCT  where BARCODE is NULL ORDER By Category ");
		int i = 0;
		while (rs.next()) {
			ImageArray[i][0] = rs.getString("PID");
			ImageArray[i][1] = rs.getString("IMAGE");
			i++;
		}
		return i;
	}

	public Connecting getConnection() {
		return Connection;
	}

	public void setConnection(Connecting connection) {
		Connection = connection;
	}

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public ResultSet getRs() {
		return rs;
	}

	public void setRs(ResultSet rs) {
		this.rs = rs;
	}

	public String[][] getImageArray() {
		return ImageArray;
	}

	public void setImageArray(String[][] imageArray) {
		ImageArray = imageArray;
	}

	public float getTva() {
		return tva;
	}

	public void setTva(float tva) {
		this.tva = tva;
	}

	public ObservableList<OrderEntry> getData() {
		return data;
	}

	public void setType(String type) {
		Type = type;
	}

}
