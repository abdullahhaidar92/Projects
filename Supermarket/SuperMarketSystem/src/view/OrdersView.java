package view;

import javafx.scene.layout.HBox;

import javafx.scene.layout.VBox;
import javafx.scene.paint.Color;
import javafx.scene.layout.FlowPane;

import javafx.scene.layout.BorderPane;

import javafx.geometry.Insets;

import javafx.geometry.Orientation;
import javafx.geometry.Pos;
import javafx.scene.control.Button;
import javafx.scene.control.TextField;

import javafx.scene.control.TableView;
import javafx.scene.control.TableColumn;
import javafx.beans.property.SimpleStringProperty;
import javafx.beans.property.SimpleIntegerProperty;
import javafx.beans.property.SimpleDoubleProperty;

import java.lang.Integer;
import java.lang.Double;
import javafx.scene.text.*;
import model.OrderEntry;
import model.OrderTableEntry;

public class OrdersView {
	private VBox SideMenu;
	private Button[] sidebuttons;
	private FlowPane[] InnerPages;
	private BorderPane OrderPane;
	private TableView<OrderTableEntry> Orders;
	private VBox buttons = new VBox();
	private Text[] info;
	private TextField OrderId;
	private TableView<OrderEntry> Order;
	private HBox bottom1 = new HBox();
	private HBox bottom2 = new HBox();
	private TextField EmployeeId, Date1, Date2;

	@SuppressWarnings("unchecked")
	public OrdersView() {

		String styleText = "-fx-font: 30px Verdana;";
       String styleButton = " -fx-font-size: 15px;-fx-font-weight: bold;-fx-body-color: pink;";
		String style2 = " -fx-font: 25px Verdana;-fx-background-color: cyan;";
		SideMenu = new VBox();
		sidebuttons = new Button[5];
		InnerPages = new FlowPane[5];
		sidebuttons[0] = new Button("All Orders");
		sidebuttons[1] = new Button("Search");
		sidebuttons[2] = new Button("  Filter By   Employee");
		sidebuttons[3] = new Button("  Filter By  \n    Date");
		sidebuttons[4] = new Button("Log Out");
		sidebuttons[2].setWrapText(true);
		sidebuttons[3].setWrapText(true);

		for (int i = 0; i < 5; i++) {
			InnerPages[i] = new FlowPane(Orientation.HORIZONTAL);
			InnerPages[i].setPrefHeight(500);
			InnerPages[i].setPrefWidth(700);
			sidebuttons[i].setPrefHeight(100);
			sidebuttons[i].setPrefWidth(100);
			sidebuttons[i].setStyle(styleButton);
			SideMenu.getChildren().add(sidebuttons[i]);
			sidebuttons[i].setAlignment(Pos.CENTER);
		}

		OrderPane = new BorderPane();
		OrderPane.setPrefWidth(1000);
		OrderPane.setPrefHeight(500);
		OrderPane.setLeft(SideMenu);

		Orders = new TableView<OrderTableEntry>();
		InnerPages[0].getChildren().add(Orders);
		TableColumn<OrderTableEntry, Integer> OID = new TableColumn<OrderTableEntry, Integer>("Order Id");
		TableColumn<OrderTableEntry, String> CID = new TableColumn<OrderTableEntry, String>("Customer Id ");
		TableColumn<OrderTableEntry, String> Cname = new TableColumn<OrderTableEntry, String>("Customer Name ");
		TableColumn<OrderTableEntry, String> EID = new TableColumn<OrderTableEntry, String>("EmployeeId");
		TableColumn<OrderTableEntry, String> Ename = new TableColumn<OrderTableEntry, String>("Employee Name");
		TableColumn<OrderTableEntry, Double> Total = new TableColumn<OrderTableEntry, Double>("Total ");
		TableColumn<OrderTableEntry, String> Total_Currancy = new TableColumn<OrderTableEntry, String>("Currancy ");
		TableColumn<OrderTableEntry, String> Date = new TableColumn<OrderTableEntry, String>("Date");
		Orders.getColumns().addAll(OID, CID, Cname, EID, Ename, Total, Total_Currancy, Date);
		// OID.setStyle("-fx-alignment: center-left;" );
		// CID.setStyle("-fx-alignment: center-left;" );
		// EID.setStyle("-fx-alignment: center-left;" );
		Orders.setPrefWidth(1200);
		Orders.setPrefHeight(500);
		OID.setPrefWidth(80);
		EID.setPrefWidth(155);
		CID.setPrefWidth(155);
		Cname.setPrefWidth(200);
		Ename.setPrefWidth(200);
		Total.setPrefWidth(150);
		Total_Currancy.setPrefWidth(100);
		Date.setPrefWidth(150);
		OID.setCellValueFactory(c -> new SimpleIntegerProperty(c.getValue().getOID()).asObject());
		CID.setCellValueFactory(c -> new SimpleStringProperty(c.getValue().getCID()));
		EID.setCellValueFactory(c -> new SimpleStringProperty(c.getValue().getEID()));
		Cname.setCellValueFactory(c -> new SimpleStringProperty(c.getValue().getCustomerName()));
		Ename.setCellValueFactory(c -> new SimpleStringProperty(c.getValue().getEmployeeName()));
		Total.setCellValueFactory(c -> new SimpleDoubleProperty(c.getValue().getTotal()).asObject());
		Total_Currancy.setCellValueFactory(c -> new SimpleStringProperty(c.getValue().getCurrancy()));
		Date.setCellValueFactory(c -> new SimpleStringProperty(c.getValue().getDate()));

		InnerPages[0].getStylesheets().add(getClass().getResource("/css/application1.css").toExternalForm());
		InnerPages[0].setPadding(new Insets(1, 10, 10, 10));

		VBox V1 = new VBox();
		VBox V2 = new VBox();
		VBox V3 = new VBox();

		info = new Text[12];

		for (int i = 0; i < 12; i++) {
			info[i] = new Text();
			info[i].setStyle(styleText);
		}
		for (int i = 0; i < 6; i++) {
			info[i].setFill(Color.BLUEVIOLET);
		}
		info[11].setFill(Color.BLUEVIOLET);
		OrderId = new TextField();
		OrderId.setStyle(style2);
		info[0].setText("Order Id");
		V1.getChildren().addAll(info[0], info[1], info[2], info[3], info[4], info[5]);
		V2.getChildren().addAll(OrderId, info[6], info[7], info[8], info[9], info[10]);
		V1.setSpacing(40);
		V2.setSpacing(40);
		V3.setSpacing(20);
		V1.setPadding(new Insets(10, 50, 10, 10));
		OrderId.setMaxWidth(200);
		InnerPages[1].getChildren().addAll(V1, V2, V3);
		InnerPages[1].setHgap(40);

		Order = new TableView<OrderEntry>();
		Order.setVisible(false);
		V3.getChildren().addAll(info[11], Order);
		TableColumn<OrderEntry, String> Name = new TableColumn<OrderEntry, String>("Product");
		TableColumn<OrderEntry, Integer> Quantity = new TableColumn<OrderEntry, Integer>("Quantity");
		TableColumn<OrderEntry, Double> Unit_Price = new TableColumn<OrderEntry, Double>("Unit Price");
		TableColumn<OrderEntry, String> Currancy = new TableColumn<OrderEntry, String>("   ");
		Order.getColumns().addAll(Name, Quantity, Unit_Price, Currancy);

		Order.setPrefWidth(670);
		Name.setPrefWidth(340);
		Quantity.setPrefWidth(95);
		Unit_Price.setPrefWidth(150);
		Currancy.setPrefWidth(85);

		Name.setCellValueFactory(c -> new SimpleStringProperty(c.getValue().getName()));
		Quantity.setCellValueFactory(c -> new SimpleIntegerProperty(c.getValue().getQuantity()).asObject());
		Unit_Price.setCellValueFactory(c -> new SimpleDoubleProperty(c.getValue().getUnit_Price()).asObject());
		Currancy.setCellValueFactory(c -> new SimpleStringProperty(c.getValue().getCurrancy()));
		Text EnterEmployee = new Text("Enter Employee Id : ");
		Text DateBetween = new Text("Date between : ");
		Text and = new Text(" and ");
		EnterEmployee.setStyle(styleText);
		DateBetween.setStyle(styleText);
		and.setStyle(styleText);
		EmployeeId = new TextField();
		Date1 = new TextField();
		Date2 = new TextField();
		EmployeeId.setStyle(style2);
		Date1.setStyle(style2);
		Date2.setStyle(style2);
		InnerPages[0].setVgap(10);
		bottom1.getChildren().addAll(EnterEmployee, EmployeeId);
		bottom2.getChildren().addAll(DateBetween, Date1, and, Date2);
		bottom1.setSpacing(40);
		bottom2.setSpacing(40);

	}

	public VBox getSideMenu() {
		return SideMenu;
	}

	public void setSideMenu(VBox sideMenu) {
		SideMenu = sideMenu;
	}

	public Button[] getSidebuttons() {
		return sidebuttons;
	}

	public void setSidebuttons(Button[] sidebuttons) {
		this.sidebuttons = sidebuttons;
	}

	public FlowPane[] getInnerPages() {
		return InnerPages;
	}

	public void setInnerPages(FlowPane[] innerPages) {
		InnerPages = innerPages;
	}

	public BorderPane getOrderPane() {
		return OrderPane;
	}

	public void setOrderPane(BorderPane orderPane) {
		OrderPane = orderPane;
	}

	public TableView<OrderTableEntry> getOrders() {
		return Orders;
	}

	public void setOrders(TableView<OrderTableEntry> orders) {
		Orders = orders;
	}

	public VBox getButtons() {
		return buttons;
	}

	public void setButtons(VBox buttons) {
		this.buttons = buttons;
	}

	public Text[] getInfo() {
		return info;
	}

	public void setInfo(Text[] info) {
		this.info = info;
	}

	public TextField getOrderId() {
		return OrderId;
	}

	public void setOrderId(TextField orderId) {
		OrderId = orderId;
	}

	public TableView<OrderEntry> getOrder() {
		return Order;
	}

	public void setOrder(TableView<OrderEntry> order) {
		Order = order;
	}

	public HBox getBottom1() {
		return bottom1;
	}

	public void setBottom1(HBox bottom1) {
		this.bottom1 = bottom1;
	}

	public HBox getBottom2() {
		return bottom2;
	}

	public void setBottom2(HBox bottom2) {
		this.bottom2 = bottom2;
	}

	public TextField getEmployeeId() {
		return EmployeeId;
	}

	public void setEmployeeId(TextField employeeId) {
		EmployeeId = employeeId;
	}

	public TextField getDate1() {
		return Date1;
	}

	public void setDate1(TextField date1) {
		Date1 = date1;
	}

	public TextField getDate2() {
		return Date2;
	}

	public void setDate2(TextField date2) {
		Date2 = date2;
	}

}
