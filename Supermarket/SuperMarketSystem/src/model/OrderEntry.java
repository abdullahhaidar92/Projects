package model;
import javafx.beans.property.SimpleStringProperty;
import javafx.beans.property.SimpleIntegerProperty;
import javafx.beans.property.SimpleDoubleProperty;
public class OrderEntry {

	 private final SimpleStringProperty Name;
	 private final SimpleIntegerProperty Quantity;
	private final SimpleStringProperty TVA;
	private final SimpleDoubleProperty  Unit_Price;
	private final SimpleDoubleProperty  Total;
	private final SimpleStringProperty currancy;
	private String Pid;
	
	public String getPid() {
		return Pid;
	}
	public void setPid(String pid) {
		Pid = pid;
	}
	public String getCurrancy() {
		return currancy.get();
	}
	public void setCurrancy(String currancy) {
		this.currancy.set(currancy);
	}
	public String getName() {
		return Name.get();
	}
	public void setName(String name) {
		Name.set(name);
	}
	public Integer getQuantity() {
		return Quantity.get();
	}
	public void setQuantity(int quantity) {
		Quantity.set(quantity);
	}
	public String getTVA() {
		return TVA.get();
	}
	public void setTVA(String tVA) {
		TVA.set(tVA);
	}
	public OrderEntry(String pid,String name, int quantity, String tVA, double unit_Price,String c) {
		super();
		Name = new SimpleStringProperty(name);
		Quantity = new SimpleIntegerProperty(quantity);
		TVA = new SimpleStringProperty(tVA);
		Unit_Price = new SimpleDoubleProperty(unit_Price);
		Total =new SimpleDoubleProperty(unit_Price*quantity);
		currancy=new SimpleStringProperty(c);
		Pid=pid;

	}
	public double getUnit_Price() {
		return Unit_Price.get();
	}
	public void setUnit_Price(double unit_Price) {
		Unit_Price.set(unit_Price);
	}
	public double getTotal() {
		return Total.get();
	}
	public void setTotal(double total) {
		Total.set(total);
	}
	
	
	
}
