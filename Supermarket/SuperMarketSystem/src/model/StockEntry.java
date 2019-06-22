package model;

import javafx.beans.property.SimpleStringProperty;
import javafx.beans.property.SimpleIntegerProperty;


public class StockEntry {
	private final SimpleStringProperty SID;
	private final SimpleStringProperty Name;
	private final SimpleIntegerProperty MinQuantity;
	private final SimpleIntegerProperty MaxQuantity;
	private final SimpleIntegerProperty Quantity;
	private String Pid;
	private String SupId;
   public String getSupId() {
		return SupId;
	}

	public void setSupId(String supId) {
		SupId = supId;
	}

public String getPid() {
		return Pid;
	}

	public void setPid(String pid) {
		Pid = pid;
	}

	

	public String getName() {
		return Name.get();
	}

	public void setName(String name) {
		Name.set(name);
	}

	
	public void setSID(String sid) {
		SID.set(sid);
	}


	public Integer getQuantity() {
		return Quantity.get();
	}

	public void setQuantity(int quantity) {
		Quantity.set(quantity);
	}
	public void setMinQuantity(int quantity) {
		MinQuantity.set(quantity);
	}
	public void setMaxQuantity(int quantity) {
		MaxQuantity.set(quantity);
	}

	public StockEntry(String sID,String name, int minquantity,int maxquantity, int quantity,String sp) {
			super();
			SID=new SimpleStringProperty(sID);
			Name = new SimpleStringProperty(name);
			MinQuantity = new SimpleIntegerProperty(minquantity);
			MaxQuantity = new SimpleIntegerProperty(maxquantity);
			Quantity = new SimpleIntegerProperty(quantity);
			SupId=sp;
	}

	public String getSID() {
		return SID.get();
	}

	public Integer getMinQuantity() {
		return MinQuantity.get();
	}

	public Integer getMaxQuantity() {
		return MaxQuantity.get();
	}

	

}
