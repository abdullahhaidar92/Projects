package view;
import javafx.scene.layout.HBox;

import javafx.scene.paint.Color;
import javafx.scene.layout.FlowPane;
import javafx.scene.layout.BorderPane;
import javafx.scene.layout.StackPane;
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

import  java.lang.Integer;
import  java.lang.Double;
import javafx.scene.text.*;
import model.OrderEntry;
import javafx.scene.image.*;
public class Home {
	private BorderPane root;
	private HBox top_menu,bottom;
	private FlowPane ImageButtons ;
	private BorderPane home_center,center;
	private Button [] top;
	private Button [] images;
	private TextField total,barcode,quantity,customer;
	private TableView<OrderEntry> Order;
	 
     Button GenerateBill,Cancel,Something;
	Button add;
	//<TextField  fx:id="Id"   onAction="#ShowGrades"  layoutX="270.0" layoutY="53.0" prefHeight="50.0" prefWidth="231.0" AnchorPane.bottomAnchor="390.0" AnchorPane.leftAnchor="250.0" AnchorPane.rightAnchor="188.0" AnchorPane.topAnchor="53.0" ></TextField>
	@SuppressWarnings("unchecked")
	public Home() {
		String style1=" -fx-font-size: 35px;-fx-body-color: blue;-fx-text-fill: yellow";
		String style2=" -fx-font-size: 25px;";
		root=new BorderPane();
		top_menu=new HBox();
		ImageButtons=new FlowPane();
		center=new BorderPane();
		//center.setMaxWidth(800);
		ImageButtons.setMinWidth(470);
		root.setTop(top_menu);
		//root.setCenter(center);
		//root.setRight(ImageButtons);
		//root.setPadding(new Insets(10,10,10,10));
		top=new Button[6];
		for(int i=0;i<6;i++) {
			top[i]=new Button();
			top[i].setMinHeight(100);
			top[i].setMinWidth(195);
			top[i].setStyle(style1);
		}
		top[0].setText("Home");
		top[1].setText("Orders");
		top[2].setText("Stock");
		top[3].setText("Products");	
		top[4].setText("Employees");
		 top[5].setText("Exit");
		images=new Button[20];
		String imageUrl = "file:///C:/Users/hp/Desktop/SuperMarketProducts/default.jpg";
		for(int i=0;i<20;i++) {
			images[i]=new Button();
			images[i].setMaxWidth(110);
			images[i].setMaxHeight(110);
			images[i].setGraphic(new ImageView(new Image(imageUrl)));
		}
		
		//C:\Users\hp\Desktop\SuperMarketProducts
	

		top_menu.getChildren().addAll(top);
		top_menu.setPadding(new Insets(20,20,20,20));
		top_menu.setSpacing(20);
		ImageButtons.setOrientation(Orientation.HORIZONTAL);
		ImageButtons.getChildren().addAll(images);
		//ImageButtons.setPadding(new Insets(20,20,20,20));
		StackPane S=new StackPane();
		total=new TextField();
		total.setAlignment(Pos.CENTER);
		Text textCurrancy=new Text("LLP    ");
		S.getChildren().addAll(total,textCurrancy);
		StackPane.setAlignment(textCurrancy,Pos.CENTER_RIGHT);
	   textCurrancy.setStyle("-fx-font-size:40pt");
	   textCurrancy.setFill(Color.RED);
		total.setMinWidth(400);
		total.setStyle("-fx-font-size:60pt");
		total.setMaxHeight(170);
		
		bottom=new HBox();
		barcode=new TextField();
		quantity=new TextField();
		customer=new TextField("C100");
		add=new Button("ADD");
		Text code=new Text("Code");
		Text customertxt=new Text("Customer");
		Text Quantit=new Text("Quantity");
		bottom.getChildren().addAll(add,code,barcode,Quantit,quantity,customertxt,customer);
		bottom.setAlignment(Pos.CENTER_LEFT);
		bottom.setSpacing(5);
		bottom.setStyle("-fx-background-color: palegreen;-fx-font-weight: bold");
		
		
		center.setTop(S);
	     
		Order=new TableView<OrderEntry>();
		center.setCenter(Order);
		//VBox V=new VBox();
	
		home_center=new BorderPane();
		root.setCenter(home_center);
		home_center.setCenter(center);
		home_center.setRight(ImageButtons);
		HBox H1=new HBox();
        GenerateBill=new Button("Generate Bill");
        Cancel=new Button("Cancel");
		 Something=new Button("Remove");
		 GenerateBill.setStyle(style2);
	        Cancel.setStyle(style2);
	        Something.setStyle(style2);
		 GenerateBill.setPrefHeight(50);
		 Cancel.setPrefHeight(50);
		 Something.setPrefHeight(50);
		 GenerateBill.setPrefWidth(500);
		 Cancel.setPrefWidth(200);
		 Something.setPrefWidth(200);
		  H1.getChildren().addAll(Cancel,GenerateBill,Something);
		  
		  
		  //V.getChildren().addAll(H1,bottom);
			center.setBottom(H1);
		     home_center.setBottom(bottom);

        TableColumn<OrderEntry, String> Name = new TableColumn<OrderEntry, String> ("Product");
        TableColumn<OrderEntry, Integer> Quantity= new TableColumn<OrderEntry, Integer>  ("Quantity");
        TableColumn<OrderEntry, String> TVA= new TableColumn<OrderEntry, String>  ("TVA");
        TableColumn<OrderEntry, Double> Unit_Price= new TableColumn<OrderEntry, Double>  ("Unit Price");
        TableColumn<OrderEntry, Double> Price= new TableColumn<OrderEntry, Double>  ("Price");
        TableColumn<OrderEntry, String> Currancy= new TableColumn<OrderEntry, String>  ("   ");
         Order.getColumns().addAll(Name,Quantity,TVA,Unit_Price,Price,Currancy);
         
         Order.setPrefWidth(890);
         Name.setPrefWidth(320);
         Quantity.setPrefWidth(95);
         TVA.setPrefWidth(100);
         Unit_Price.setPrefWidth(150);
         Price.setPrefWidth(170);
         Currancy.setPrefWidth(55);
      
       Name.setCellValueFactory(
    		   c-> new SimpleStringProperty(c.getValue().getName()));
       Quantity.setCellValueFactory(
    		   c-> new SimpleIntegerProperty(c.getValue().getQuantity()).asObject());
       TVA.setCellValueFactory(
    		   c-> new SimpleStringProperty(c.getValue().getTVA()));
    Unit_Price.setCellValueFactory(
    		   c-> new SimpleDoubleProperty(c.getValue().getUnit_Price()).asObject());
    Price.setCellValueFactory(
 		   c-> new SimpleDoubleProperty(c.getValue().getTotal()).asObject());
    Currancy.setCellValueFactory(
 		   c-> new SimpleStringProperty(c.getValue().getCurrancy()));
    
    
    }
	
	
	
	public BorderPane getRoot() {
		return root;
	}
	public void setRoot(BorderPane root) {
		this.root = root;
	}
	public HBox getTop_menu() {
		return top_menu;
	}
	public void setTop_menu(HBox top_menu) {
		this.top_menu = top_menu;
	}
	public HBox getBottom() {
		return bottom;
	}
	public void setBottom(HBox bottom) {
		this.bottom = bottom;
	}
	public FlowPane getImageButtons() {
		return ImageButtons;
	}
	public void setImageButtons(FlowPane imageButtons) {
		ImageButtons = imageButtons;
	}
	public BorderPane getHome_center() {
		return home_center;
	}
	public void setHome_center(BorderPane home_center) {
		this.home_center = home_center;
	}
	public BorderPane getCenter() {
		return center;
	}
	public void setCenter(BorderPane center) {
		this.center = center;
	}
	public Button[] getTop() {
		return top;
	}
	public void setTop(Button[] top) {
		this.top = top;
	}
	public Button[] getImages() {
		return images;
	}
	public void setImages(Button[] images) {
		this.images = images;
	}
	public TextField getTotal() {
		return total;
	}
	public void setTotal(TextField total) {
		this.total = total;
	}
	public TextField getBarcode() {
		return barcode;
	}
	public void setBarcode(TextField barcode) {
		this.barcode = barcode;
	}
	public TextField getQuantity() {
		return quantity;
	}
	public void setQuantity(TextField quantity) {
		this.quantity = quantity;
	}
	public TextField getCustomer() {
		return customer;
	}
	public void setCustomer(TextField customer) {
		this.customer = customer;
	}
	public TableView<OrderEntry> getOrder() {
		return Order;
	}
	public void setOrder(TableView<OrderEntry> order) {
		Order = order;
	}
	public Button getGenerateBill() {
		return GenerateBill;
	}
	public void setGenerateBill(Button generateBill) {
		GenerateBill = generateBill;
	}
	public Button getCancel() {
		return Cancel;
	}
	public void setCancel(Button cancel) {
		Cancel = cancel;
	}
	public Button getSomething() {
		return Something;
	}
	public void setSomething(Button something) {
		Something = something;
	}
	public Button getAdd() {
		return add;
	}
	public void setAdd(Button add) {
		this.add = add;
	}
	
	
	
	

}
