package model;
import javafx.beans.property.SimpleStringProperty;
import javafx.beans.property.SimpleIntegerProperty;
import javafx.beans.property.SimpleDoubleProperty;
public class OrderTableEntry {

	 private final SimpleIntegerProperty OID;
	 private final SimpleStringProperty CID;
	 private final SimpleStringProperty CustomerName;
	 private final SimpleStringProperty EID;
	 private final SimpleStringProperty EmployeeName;
	 private final SimpleDoubleProperty  Total;
		private final SimpleStringProperty Total_Currancy;
	 private final SimpleStringProperty Date;

	
	
	public String getCurrancy() {
		return Total_Currancy.get();
	}
	public void setCurrancy(String currancy) {
		this.Total_Currancy.set(currancy);
	}
	public int getOID() {
		return OID.get();
	}
	public String getCustomerName() {
		return CustomerName.get();
	}
	
	public String getDate() {
		return Date.get();
	}
	public String getEmployeeName() {
		return EmployeeName.get();
	}
	public void setEmployeeName(String name) {
		EmployeeName.set(name);
	}
	public String getCID() {
		return CID.get();
	}
	public void setCID(String x) {
		CID .set(x);
	}
	public double getTotal() {
		return Total.get();
	}
	public void setTotal(double x) {
		Total.set(x);
	}
	public String getEID() {
		return EID.get();
	}
	public void setEID(String t) {
		EID.set(t);
	}
	public OrderTableEntry(int oID, String cID, String customerName,
			String eID, String employeeName, Double total,
			String total_Currancy, String date) {
		super();
		OID = new SimpleIntegerProperty(oID);
		CID =new SimpleStringProperty(cID);
		CustomerName = new SimpleStringProperty(customerName);
		EID =  new SimpleStringProperty(eID);
		EmployeeName =  new SimpleStringProperty(employeeName);
		Total =  new SimpleDoubleProperty(total);
		Total_Currancy = new SimpleStringProperty(total_Currancy);
		Date =  new SimpleStringProperty(date);
	}
	



	
	
}
