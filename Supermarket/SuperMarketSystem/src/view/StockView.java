package view;

import javafx.scene.layout.HBox;

import javafx.scene.layout.VBox;

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

import java.lang.Integer;

import model.StockEntry;


import javafx.scene.text.*;

public class StockView {
	private VBox SideMenu;
	private Button[] sidebuttons;
	private FlowPane[] InnerPages;
	private BorderPane StockPane;
	private Button b1,ADD,AddToStock;
	private HBox[] Rows;
	private Button [] suppliers;
	private TableView<StockEntry> Stocks;
	private VBox buttons=new VBox();
	private TextField [] Input;
	private TextField stockid,stockq,stockid2;
	private Text note,stid,stq;
	private Text [] info;
	@SuppressWarnings("unchecked")
	public StockView() {
		
		String styleText= "-fx-font: 35px Verdana;";
		String style1=" -fx-font-size: 14px;-fx-body-color: red;";
		String styleButton = " -fx-font-size: 15px;-fx-font-weight: bold;-fx-body-color: pink;";
		String styleButton1 = " -fx-font-size: 45px;-fx-body-color: pink;";
		String style2=" -fx-font: 35px Verdana;-fx-background-color: cyan;";
		SideMenu = new VBox();
		sidebuttons = new Button[5];
		InnerPages = new FlowPane[5];
		b1 = new Button("Button1");
		sidebuttons[0] = new Button("All Stocks");
		sidebuttons[1] = new Button("Empty \nStocks");
		sidebuttons[2] = new Button("Add Stock");
		sidebuttons[3] = new Button("Add To \n Stock");
		sidebuttons[4] = new Button("Search");
		sidebuttons[1].setWrapText(true);
		sidebuttons[3].setWrapText(true);
		for (int i = 0; i < 5; i++) {
			InnerPages[i] = new FlowPane(Orientation.HORIZONTAL);
			
			InnerPages[i].setPrefHeight(500);
			InnerPages[i].setPrefWidth(700);
			sidebuttons[i].setPrefHeight(100);
			sidebuttons[i].setPrefWidth(100);
			sidebuttons[i].setStyle(styleButton);
			SideMenu.getChildren().add(sidebuttons[i]);
		}

		StockPane = new BorderPane();
		StockPane.setPrefWidth(1000);
		StockPane.setPrefHeight(500);
		StockPane.setLeft(SideMenu);

		
		
		
		
			
			Stocks=new TableView<StockEntry>();
			InnerPages[0].getChildren().add(Stocks);
			 TableColumn<StockEntry, String> SID = new TableColumn<StockEntry, String> ("SID");
		    TableColumn<StockEntry, String> Name = new TableColumn<StockEntry, String> ("Name");
	        TableColumn<StockEntry, Integer> MinQuantity= new TableColumn<StockEntry, Integer>  ("MinQuantity");
	        TableColumn<StockEntry, Integer> MaxQuantity= new TableColumn<StockEntry, Integer>  ("MaxQuantity");
	        TableColumn<StockEntry, Integer> Quantity= new TableColumn<StockEntry, Integer>  ("Quantity            ");
	        
	         Stocks.getColumns().addAll(SID,Name,MinQuantity,MaxQuantity,Quantity);
	         
	        Stocks.setPrefWidth(890);
	        Stocks.setPrefHeight(500);
	         Name.setPrefWidth(270);
	         MinQuantity.setPrefWidth(200);
	        MaxQuantity.setPrefWidth(200);
	        Quantity.setPrefWidth(200);
	       SID.setCellValueFactory(
		    		   c-> new SimpleStringProperty(c.getValue().getSID()));
	       Name.setCellValueFactory(
	    		   c-> new SimpleStringProperty(c.getValue().getName()));
	       Quantity.setCellValueFactory(
	    		   c-> new SimpleIntegerProperty(c.getValue().getQuantity()).asObject());
	       MinQuantity.setCellValueFactory(
	    		   c-> new SimpleIntegerProperty(c.getValue().getMinQuantity()).asObject());
	       MaxQuantity.setCellValueFactory(
	    		   c-> new SimpleIntegerProperty(c.getValue().getMaxQuantity()).asObject());
			
			
			InnerPages[0].getStylesheets().add(getClass().getResource("/css/application1.css").toExternalForm());
			InnerPages[0].setPadding(new Insets(10,10,10,10));
			suppliers=new Button[15];
			InnerPages[0].getChildren().add(buttons);
			for(int i=0;i<15;i++) {
			suppliers[i]=new Button(" View Supplier Information");
				suppliers[i].setStyle(style1);
				suppliers[i].setMaxHeight(17);
				suppliers[i].setMinWidth(60);
			buttons.getChildren().add(suppliers[i]);
			}
			suppliers[0].setVisible(false);
			
			
			
			
			
			VBox V1=new VBox();
			VBox V2=new VBox();
			Text [] Header=new Text[5];
			Header[0]=new Text("Stock Id");
			Header[1]=new Text("Product Id");
			Header[2]=new Text("Quantity");
			Header[3]=new Text("Minimum Quantity");
			Header[4]=new Text("Maximum Quantity");
			V1.getChildren().addAll(Header);
			V1.setSpacing(50);
			V2.setSpacing(20);
			for(int i=0;i<5;i++) {
				Header[i].setStyle(styleText);
			}
			Input=new TextField[5];
			for(int i=0;i<5;i++) {
				Input[i]=new TextField();
				Input[i].setStyle(style2);
			}
			V2.getChildren().addAll(Input);
			InnerPages[1].getChildren().addAll(V1,V2);
			InnerPages[1].setPadding(new Insets(40,20,20,20));
			InnerPages[1].setHgap(40);
			ADD=new Button("ADD");
			ADD.setStyle(styleButton1);
			VBox V3=new VBox();
			V3.getChildren().add(ADD);
			V3.setAlignment(Pos.BOTTOM_CENTER);
			V3.setPadding(new Insets(20,20,20,20));
			InnerPages[1].getChildren().add(V3);
			
			HBox H1=new HBox();
			HBox H2=new HBox();
			HBox H3=new HBox();
			HBox H4=new HBox();
			stockid=new TextField();
			stockq=new TextField();
			note=new Text("");
			stid=new Text("Sock Id");
			stq=new Text("Quantity");
			AddToStock=new Button("ADD");
			H1.getChildren().addAll(stid,stockid);
			H2.getChildren().addAll(stq,stockq);
			H3.getChildren().add(AddToStock);
			H4.getChildren().add(note);
			InnerPages[2].getChildren().addAll(H1,H2,H3,H4);
			InnerPages[2].setPadding(new Insets(40,40,40,40));
			InnerPages[2].setVgap(20);
			InnerPages[2].setOrientation(Orientation.VERTICAL);
			H1.setSpacing(95);
			H2.setSpacing(80);
	        H3.setAlignment(Pos.CENTER_RIGHT);
	        H4.setAlignment(Pos.CENTER);
	        stockid.setStyle(style2);
			stockq.setStyle(style2);
			note.setStyle(styleText);
			stid.setStyle(styleText);
			stq.setStyle(styleText);
			AddToStock.setStyle(styleButton1);
	        
			info=new Text[9];
			for(int i=0;i<9;i++) {
				info[i]=new Text();
				info[i].setStyle(styleText);
			}
			stockid2=new TextField();
			stockid2.setStyle(style2);
			info[0].setText("Stock Id");
			
			VBox V5=new VBox();
			VBox V6=new VBox();
			V5.getChildren().addAll(info[0],info[1],info[2],info[3],info[4]);
			V6.getChildren().addAll(stockid2,info[5],info[6],info[7],info[8]);
			V5.setSpacing(40);
			V6.setSpacing(40);
			InnerPages[3].getChildren().addAll(V5,V6);
			InnerPages[3].setHgap(40);
			InnerPages[3].setPadding(new Insets(10,10,10,10));
			V5.setPadding(new Insets(20,10,10,10));
	}
			
			
			
			/////////////////////getters and setters ///////////////////////////
			
			
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
	public BorderPane getStockPane() {
		return StockPane;
	}
	public void setStockPane(BorderPane stockPane) {
		StockPane = stockPane;
	}
	public Button getB1() {
		return b1;
	}
	public void setB1(Button b1) {
		this.b1 = b1;
	}
	public Button getADD() {
		return ADD;
	}
	public void setADD(Button aDD) {
		ADD = aDD;
	}
	public Button getAddToStock() {
		return AddToStock;
	}
	public void setAddToStock(Button addToStock) {
		AddToStock = addToStock;
	}
	public HBox[] getRows() {
		return Rows;
	}
	public void setRows(HBox[] rows) {
		Rows = rows;
	}
	public Button[] getSuppliers() {
		return suppliers;
	}
	public void setSuppliers(Button[] suppliers) {
		this.suppliers = suppliers;
	}
	public TableView<StockEntry> getStocks() {
		return Stocks;
	}
	public void setStocks(TableView<StockEntry> stocks) {
		Stocks = stocks;
	}
	public VBox getButtons() {
		return buttons;
	}
	public void setButtons(VBox buttons) {
		this.buttons = buttons;
	}
	public TextField[] getInput() {
		return Input;
	}
	public void setInput(TextField[] input) {
		Input = input;
	}
	public TextField getStockid() {
		return stockid;
	}
	public void setStockid(TextField stockid) {
		this.stockid = stockid;
	}
	public TextField getStockq() {
		return stockq;
	}
	public void setStockq(TextField stockq) {
		this.stockq = stockq;
	}
	public TextField getStockid2() {
		return stockid2;
	}
	public void setStockid2(TextField stockid2) {
		this.stockid2 = stockid2;
	}
	public Text getNote() {
		return note;
	}
	public void setNote(Text note) {
		this.note = note;
	}
	public Text getStid() {
		return stid;
	}
	public void setStid(Text stid) {
		this.stid = stid;
	}
	public Text getStq() {
		return stq;
	}
	public void setStq(Text stq) {
		this.stq = stq;
	}
	public Text[] getInfo() {
		return info;
	}
	public void setInfo(Text[] info) {
		this.info = info;
	}
	     
	}


