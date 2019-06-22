package view;

import javafx.scene.layout.HBox;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.layout.VBox;

import javafx.scene.layout.FlowPane;

import javafx.scene.layout.BorderPane;

import javafx.geometry.Insets;

import javafx.geometry.Orientation;
import javafx.geometry.Pos;
import javafx.scene.control.Button;

import javafx.scene.control.TableView;
import javafx.scene.control.TableColumn;
import javafx.beans.property.SimpleStringProperty;
import javafx.beans.property.SimpleIntegerProperty;
import javafx.beans.property.SimpleDoubleProperty;

import java.lang.Integer;
import java.lang.Double;
import javafx.scene.text.*;
import model.OrderEntry;
public class BillView {
	private FlowPane root;
	private Button Print = new Button("Print");
	public BorderPane Top = new BorderPane();
	// HBox H1=new HBox();
	private VBox V1, V2, V3, V4;
	private  Text orderId;
	private  Text customerId;
	private  Text date;
	private  Text employeeId;
	private  Text total;
	private  Text tva;
	private  TableView<OrderEntry> Order;
	// VBox V1=new VBox();
	private ImageView Logo = new ImageView(new Image("file:///C:/Users/hp/Desktop/SuperMarketProducts/super.png"));

	@SuppressWarnings("unchecked")
	public BillView() {
		
		String style = "-fx-font: 15px Verdana;";
		String style1 = "-fx-font: 25px Verdana;";
		String style2 = "-fx-font: 45px Verdana;";
		root = new FlowPane(Orientation.VERTICAL);
		root.setStyle("-fx-border-color: black;-fx-border-width: 5px;");
		V1 = new VBox();
		V2 = new VBox();
		V3 = new VBox();
		V4 = new VBox();
		V1.setPadding(new Insets(5, 5, 5, 5));
		V2.setPadding(new Insets(5, 5, 5, 5));
		Text Total1 = new Text("Total : ");
		Text TVA1 = new Text("TVA :  ");
		total = new Text("11111111");
		tva = new Text("11111%");
		Text ordernb = new Text("Order# : ");
		orderId = new Text("111111");
		V1.getChildren().add(ordernb);
		V2.getChildren().add(orderId);
		V1.setSpacing(5);
		V2.setSpacing(5);
		Text customer = new Text("Customer : ");
		customerId = new Text("111111");
		V1.getChildren().add(customer);
		V2.getChildren().add(customerId);
		Text Date = new Text("Date ");
		date = new Text("11111111");
		V1.getChildren().add(Date);
		V2.getChildren().add(date);
		Text Employee = new Text("Employee ");
		employeeId = new Text("11111111");
		V1.getChildren().add(Employee);
		V2.getChildren().add(employeeId);

		orderId.setStyle(style);
		ordernb.setStyle(style);

		customer.setStyle(style);
		Employee.setStyle(style);
		Date.setStyle(style);
		orderId.setStyle(style);
		customerId.setStyle(style);
		date.setStyle(style);
		employeeId.setStyle(style);
		total.setStyle(style2);
		tva.setStyle(style1);
		Total1.setStyle(style1);
		TVA1.setStyle(style1);
		Top.setLeft(V1);
		Top.setCenter(V2);
		Top.setRight(Logo);
		root.getChildren().add(Top);
		Order = new TableView<OrderEntry>();
		TableColumn<OrderEntry, String> Name = new TableColumn<OrderEntry, String>("Product");
		TableColumn<OrderEntry, Integer> Quantity = new TableColumn<OrderEntry, Integer>("Quantity");
		TableColumn<OrderEntry, String> TVA = new TableColumn<OrderEntry, String>("TVA");
		TableColumn<OrderEntry, Double> Unit_Price = new TableColumn<OrderEntry, Double>("Unit Price");
		TableColumn<OrderEntry, Double> Price = new TableColumn<OrderEntry, Double>("Price");
		TableColumn<OrderEntry, String> Currancy = new TableColumn<OrderEntry, String>("   ");
		Order.getColumns().addAll(Name, Quantity, TVA, Unit_Price, Price, Currancy);
		Order.setPrefWidth(520);
		Name.setPrefWidth(170);
		Quantity.setPrefWidth(70);
		TVA.setPrefWidth(60);
		Unit_Price.setPrefWidth(90);
		Price.setPrefWidth(80);
		Currancy.setPrefWidth(40);
		V1.setStyle(" -fx-background-color: white");
		V2.setStyle(" -fx-background-color: white");
		Name.setCellValueFactory(c -> new SimpleStringProperty(c.getValue().getName()));
		Quantity.setCellValueFactory(c -> new SimpleIntegerProperty(c.getValue().getQuantity()).asObject());
		TVA.setCellValueFactory(c -> new SimpleStringProperty(c.getValue().getTVA()));
		Unit_Price.setCellValueFactory(c -> new SimpleDoubleProperty(c.getValue().getUnit_Price()).asObject());
		Price.setCellValueFactory(c -> new SimpleDoubleProperty(c.getValue().getTotal()).asObject());
		Currancy.setCellValueFactory(c -> new SimpleStringProperty(c.getValue().getCurrancy()));

		
		
		
       HBox H1=new HBox();
      HBox H2=new HBox();
      V3.getChildren().addAll(H1,H2);
        H1.getChildren().addAll(Total1,total);
        H1.setAlignment(Pos.CENTER_LEFT);
        H2.getChildren().addAll(TVA1,tva);
        Print.setMinHeight(100);
        Print.setMinWidth(100);
        Print.setStyle( "-fx-font-size: 30px;-fx-body-color: pink;");
        HBox H3=new HBox();
        V4=new VBox();
        Button Print1=new Button();
        Print1.setMinHeight(50);
        Print1.setMinWidth(100);
        Print1.setVisible(false);
        V4.getChildren().addAll(Print1,Print);
        V4.setAlignment(Pos.BOTTOM_CENTER);
        H3.getChildren().addAll(V3,V4);
      //  Print.setAlignment(Pos.BOTTOM_RIGHT);
        H3.setPadding(new Insets(10,10,10,10));
         H3.setSpacing(20);
         root.getChildren().add(Order);
	   root.getChildren().add(H3);
	
		
	}

	public FlowPane getRoot() {
		return root;
	}

	public void setRoot(FlowPane root) {
		this.root = root;
	}

	public Button getPrint() {
		return Print;
	}

	public void setPrint(Button print) {
		Print = print;
	}

	public BorderPane getTop() {
		return Top;
	}

	public void setTop(BorderPane top) {
		Top = top;
	}

	public VBox getV1() {
		return V1;
	}

	public void setV1(VBox v1) {
		V1 = v1;
	}

	public VBox getV2() {
		return V2;
	}

	public void setV2(VBox v2) {
		V2 = v2;
	}

	public VBox getV3() {
		return V3;
	}

	public void setV3(VBox v3) {
		V3 = v3;
	}

	public VBox getV4() {
		return V4;
	}

	public void setV4(VBox v4) {
		V4 = v4;
	}

	public Text getOrderId() {
		return orderId;
	}

	public void setOrderId(Text orderId) {
		this.orderId = orderId;
	}

	public Text getCustomerId() {
		return customerId;
	}

	public void setCustomerId(Text customerId) {
		this.customerId = customerId;
	}

	public Text getDate() {
		return date;
	}

	public void setDate(Text date) {
		this.date = date;
	}

	public Text getEmployeeId() {
		return employeeId;
	}

	public void setEmployeeId(Text employeeId) {
		this.employeeId = employeeId;
	}

	public Text getTotal() {
		return total;
	}

	public void setTotal(Text total) {
		this.total = total;
	}

	public Text getTva() {
		return tva;
	}

	public void setTva(Text tva) {
		this.tva = tva;
	}

	public TableView<OrderEntry> getOrder() {
		return Order;
	}

	public void setOrder(TableView<OrderEntry> order) {
		Order = order;
	}

	public ImageView getLogo() {
		return Logo;
	}

	public void setLogo(ImageView logo) {
		Logo = logo;
	}
	
	
	

}
