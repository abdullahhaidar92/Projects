package view;

import javafx.scene.layout.VBox;
import javafx.scene.paint.Color;
import javafx.scene.layout.FlowPane;
import javafx.scene.layout.BorderPane;
import javafx.geometry.Insets;
import javafx.geometry.Orientation;
import javafx.geometry.Pos;
import javafx.scene.control.Button;
import javafx.scene.control.TextField;
import javafx.scene.control.ToggleGroup;
import javafx.scene.control.PasswordField;
import javafx.scene.control.RadioButton;
import javafx.scene.control.CheckBox;
import javafx.scene.layout.HBox;
import javafx.scene.text.Text;

public class EmployeeView {
	private VBox SideMenu;
	private VBox B1, B2, B3, B6, B7, B8, B9, B10;
	private BorderPane root;
	private Button add, search, stats, visit;
	private FlowPane AddEmployeePane, All_Stats;
	private FlowPane SearchEmployeePane;
	private FlowPane EmployeeStats;
	private Text[] text1, text2, text3, text4, text5;
	private TextField[] textfield1;
	private TextField id, visitor;
	private Button A, S, all_stats;
	private RadioButton cash, stock;
	private CheckBox account;
	private final ToggleGroup group;
	private TextField username;
	private PasswordField password;
	private HBox user, pass;

	// Text
	// EID,FNAME,LNAME,BIRTHDATE,ADDRESS,PHONE,GENDER,SALARY,SALARY_Currancy,Position;
	// TextField
	// TEID,TFNAME,TLNAME,TBIRTHDATE,TADDRESS,TPHONE,TGENDER,TSALARY,TSALARY_Currancy,TPosition;
	public EmployeeView() {
		String styleText = "-fx-font: 29px Verdana;";
		String styleText1 = "-fx-font: 30px Verdana;-fx-body-color: cyan";
		String styleButton = " -fx-font-size: 15px;-fx-font-weight: bold;-fx-body-color: pink;";
		String styleButton1 = " -fx-font-size: 45px;-fx-font-weight: bold;-fx-body-color: pink;";
		String styleButton2 = " -fx-font-size: 25px;-fx-font-weight: bold;-fx-body-color: pink;";
		String style5 = " -fx-font: 25px Calibri;";
		String style6 = " -fx-font: 15px Calibri;";
		String style4 = " -fx-font: 24px Verdana;-fx-background-color: cyan;";
		String stylePane2 = " -fx-font: 35px Verdana;";
		root = new BorderPane();

		root.setStyle("-fx-background-color:white");

		SideMenu = new VBox();

		// AddEmployeePane = new FlowPane(Orientation.HORIZONTAL);
		AddEmployeePane = new FlowPane(Orientation.HORIZONTAL);
		SearchEmployeePane = new FlowPane(Orientation.VERTICAL);
		EmployeeStats = new FlowPane(Orientation.HORIZONTAL);
		All_Stats = new FlowPane(Orientation.VERTICAL);
		add = new Button("Add");
		search = new Button("Search");
		stats = new Button("Employee\nStats");
		all_stats = new Button("All\nStats");
		add.setPrefHeight(100);
		add.setPrefWidth(100);
		add.setStyle(styleButton);
		add.setAlignment(Pos.CENTER);
		stats.setPrefHeight(100);
		stats.setPrefWidth(100);
		stats.setStyle(styleButton);
		stats.setAlignment(Pos.CENTER);
		all_stats.setPrefHeight(100);
		all_stats.setPrefWidth(100);
		all_stats.setStyle(styleButton);
		all_stats.setAlignment(Pos.CENTER);
		search.setPrefHeight(100);
		search.setPrefWidth(100);
		search.setStyle(styleButton);
		search.setAlignment(Pos.CENTER);
		SideMenu.getChildren().addAll(add, search, stats, all_stats);
		root.setLeft(SideMenu);

		//////////////////////////////// First Pane
		//////////////////////////////// ////////////////////////////////////////////
		AddEmployeePane.setHgap(5);
		AddEmployeePane.setVgap(5);
		//////////////////////////////////// B1
		//////////////////////////////////// /////////////////////////////////////////////////
		B1 = new VBox();
		text1 = new Text[10];
		text1[0] = new Text("Employee Id : ");
		text1[1] = new Text("First Name : ");
		text1[2] = new Text("Last Name : ");
		text1[3] = new Text("Birthdate : ");
		text1[4] = new Text("Address : ");
		text1[5] = new Text("Phone : ");
		text1[6] = new Text("Gender : ");
		text1[7] = new Text("Salary : ");
		text1[8] = new Text("Salary Currancy : ");
		text1[9] = new Text("Position : ");
		B1.getChildren().addAll(text1);
		B1.setSpacing(20);
		///////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////// B2
		/////////////////////////////////////////////////////////////////////////////////////////// //////////////////////////////////////////////////
		B2 = new VBox();
		textfield1 = new TextField[10];
		for (int i = 0; i < 10; i++) {
			textfield1[i] = new TextField();
			textfield1[i].setStyle(style4);
			text1[i].setStyle(styleText);
		}
		B2.getChildren().addAll(textfield1);
		B2.setSpacing(10);
		///////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////// B3
		/////////////////////////////////////////////////////////////////////////////////////////// //////////////////////////////////////////////////
		B3 = new VBox();
		////////////////////////////////
		VBox B4 = new VBox();
		Text t1 = new Text("Create an account for this employee : ");
		t1.setStyle(styleText);
		account = new CheckBox("Create Account ");
		account.setStyle(styleText);
		user = new HBox();
		pass = new HBox();
		Text t = new Text("username :");
		Text tt = new Text("password  :");
		t.setStyle(style5);
		t.setFill(Color.DEEPPINK);
		tt.setStyle(style5);
		tt.setFill(Color.DEEPPINK);
		username = new TextField();
		password = new PasswordField();
		username.setStyle(style6);
		password.setStyle(style6);
		user.setDisable(true);
		pass.setDisable(true);
		user.getChildren().addAll(t, username);
		pass.getChildren().addAll(tt, password);
		user.setPadding(new Insets(0, 20, 0, 40));
		pass.setPadding(new Insets(0, 20, 0, 40));
		B4.getChildren().addAll(t1, account, user, pass);
		B4.setSpacing(20);
		//////////////////////////////
		VBox B5 = new VBox();
		Text t2 = new Text("Specify the role of this employee : ");
		t2.setStyle(styleText);
		cash = new RadioButton("Cashier");
		cash.setStyle(styleText);
		stock = new RadioButton("StockManager");
		stock.setStyle(styleText);
		group = new ToggleGroup();
		cash.setToggleGroup(group);
		cash.setUserData("Cashier");
		stock.setUserData("StockManager");
		cash.setSelected(true);
		cash.setDisable(true);
		stock.setDisable(true);
		stock.setToggleGroup(group);
		B5.getChildren().addAll(t2, cash, stock);
		B5.setSpacing(30);

		///////////////////////////////////////
		A = new Button("ADD");
		A.setStyle(styleButton1);
		A.setPrefSize(250, 100);
		B3.getChildren().addAll(B4, B5, A);
		B3.setSpacing(40);
		B3.setAlignment(Pos.CENTER);
		B3.setPadding(new Insets(10, 10, 10, 30));
		////////////////////////////////////////////////////////////////////////////////////////
		AddEmployeePane.getChildren().addAll(B1, B2, B3);
		AddEmployeePane.setPadding(new Insets(10, 10, 10, 25));

		///////////////////////////////////// Second Pane
		///////////////////////////////////// ///////////////////////////////////
		HBox H = new HBox();
		HBox H1 = new HBox();
		B6 = new VBox();
		B7 = new VBox();
		text2 = new Text[9];
		text3 = new Text[9];
		id = new TextField();
		id.setPromptText(" Employee Id                                  ");
		id.setStyle(styleText1);
		id.setPrefSize(450, 50);
		H.getChildren().add(id);
		text2[0] = new Text("First Name: ");
		text2[1] = new Text("Last Name: ");
		text2[2] = new Text("Birthdate: ");
		text2[3] = new Text("Address: ");
		text2[4] = new Text("Phone: ");
		text2[5] = new Text("Gender: ");
		text2[6] = new Text("Salary: ");
		text2[7] = new Text("Salary Currancy: ");
		text2[8] = new Text("Position: ");
		for (int i = 0; i < 9; i++) {
			text3[i] = new Text();
			text3[i].setStyle(stylePane2);
			text2[i].setStyle(stylePane2);
			text3[i].setFill(Color.RED);
		}
		SearchEmployeePane.setHgap(20);
		B6.setSpacing(10);
		B7.setSpacing(10);
		H1.getChildren().addAll(B6, B7);
		H.setPadding(new Insets(10, 10, 10, 10));
		SearchEmployeePane.getChildren().addAll(H, H1);
		H1.setPadding(new Insets(10, 10, 10, 10));
		SearchEmployeePane.setPadding(new Insets(10, 10, 10, 20));

		///////////////////////////////////// Third Pane
		///////////////////////////////////// ///////////////////////////////////
		B8 = new VBox();
		B9 = new VBox();
		B8 = new VBox();
		B9 = new VBox();
		text4 = new Text[6];
		text5 = new Text[6];
		text4[0] = new Text("ALL Employees : ");
		text4[1] = new Text("Cashiers : ");
		text4[2] = new Text("StockManagers : ");
		text4[3] = new Text("Total Salarys: ");
		text4[4] = new Text("All Orders : ");
		text4[5] = new Text("Total Sum of Orders : ");
		B8.getChildren().addAll(text4);
		for (int i = 0; i < 6; i++) {
			text5[i] = new Text();
			text4[i].setStyle(stylePane2);
			text5[i].setStyle(stylePane2);
			text5[i].setFill(Color.RED);
		}
		EmployeeStats.setHgap(40);
		B8.setSpacing(10);
		B9.setSpacing(10);
		EmployeeStats.getChildren().addAll(B8, B9);
		EmployeeStats.setPadding(new Insets(60, 40, 40, 80));

		////////////////////////// Fourth Pane /////////////////////////////////////
		All_Stats.setHgap(0);
		All_Stats.setPadding(new Insets(0, 40, 0, 80));
		HBox H3 = new HBox();
		visitor = new TextField("StatisticsRevenue");
		visitor.setPromptText(" Visitor Class ");
		visitor.setStyle(styleText1);
		visitor.setPrefSize(450, 70);
		visit = new Button("Visit");
		visit.setStyle(styleButton2);
		visit.setPrefSize(150, 70);
		H3.getChildren().addAll(visitor, visit);
		H3.setSpacing(30);
		B10 = new VBox();
		All_Stats.getChildren().addAll(H3, B10);
		B10.setSpacing(7);
	}

	public VBox getB10() {
		return B10;
	}

	public void setB10(VBox b10) {
		B10 = b10;
	}

	public Button getVisit() {
		return visit;
	}

	public void setVisit(Button visit) {
		this.visit = visit;
	}

	public FlowPane getAll_Stats() {
		return All_Stats;
	}

	public void setAll_Stats(FlowPane all_Stats) {
		All_Stats = all_Stats;
	}

	public TextField getVisitor() {
		return visitor;
	}

	public void setVisitor(TextField visitor) {
		this.visitor = visitor;
	}

	public Button getAll_stats() {
		return all_stats;
	}

	public void setAll_stats(Button all_stats) {
		this.all_stats = all_stats;
	}

	public VBox getSideMenu() {
		return SideMenu;
	}

	public void setSideMenu(VBox sideMenu) {
		SideMenu = sideMenu;
	}

	public VBox getB1() {
		return B1;
	}

	public void setB1(VBox b1) {
		B1 = b1;
	}

	public VBox getB2() {
		return B2;
	}

	public void setB2(VBox b2) {
		B2 = b2;
	}

	public VBox getB3() {
		return B3;
	}

	public void setB3(VBox b3) {
		B3 = b3;
	}

	public VBox getB6() {
		return B6;
	}

	public void setB6(VBox b6) {
		B6 = b6;
	}

	public VBox getB7() {
		return B7;
	}

	public void setB7(VBox b7) {
		B7 = b7;
	}

	public VBox getB8() {
		return B8;
	}

	public void setB8(VBox b8) {
		B8 = b8;
	}

	public VBox getB9() {
		return B9;
	}

	public void setB9(VBox b9) {
		B9 = b9;
	}

	public BorderPane getRoot() {
		return root;
	}

	public void setRoot(BorderPane root) {
		this.root = root;
	}

	public Button getAdd() {
		return add;
	}

	public void setAdd(Button add) {
		this.add = add;
	}

	public Button getSearch() {
		return search;
	}

	public void setSearch(Button search) {
		this.search = search;
	}

	public Button getStats() {
		return stats;
	}

	public void setStats(Button stats) {
		this.stats = stats;
	}

	public FlowPane getAddEmployeePane() {
		return AddEmployeePane;
	}

	public void setAddEmployeePane(FlowPane addEmployeePane) {
		AddEmployeePane = addEmployeePane;
	}

	public FlowPane getSearchEmployeePane() {
		return SearchEmployeePane;
	}

	public void setSearchEmployeePane(FlowPane searchEmployeePane) {
		SearchEmployeePane = searchEmployeePane;
	}

	public FlowPane getEmployeeStats() {
		return EmployeeStats;
	}

	public void setEmployeeStats(FlowPane employeeStats) {
		EmployeeStats = employeeStats;
	}

	public Text[] getText1() {
		return text1;
	}

	public void setText1(Text[] text1) {
		this.text1 = text1;
	}

	public Text[] getText2() {
		return text2;
	}

	public void setText2(Text[] text2) {
		this.text2 = text2;
	}

	public Text[] getText3() {
		return text3;
	}

	public void setText3(Text[] text3) {
		this.text3 = text3;
	}

	public Text[] getText4() {
		return text4;
	}

	public void setText4(Text[] text4) {
		this.text4 = text4;
	}

	public Text[] getText5() {
		return text5;
	}

	public void setText5(Text[] text5) {
		this.text5 = text5;
	}

	public TextField[] getTextfield1() {
		return textfield1;
	}

	public void setTextfield1(TextField[] textfield1) {
		this.textfield1 = textfield1;
	}

	public TextField getId() {
		return id;
	}

	public void setId(TextField id) {
		this.id = id;
	}

	public Button getA() {
		return A;
	}

	public void setA(Button a) {
		A = a;
	}

	public Button getS() {
		return S;
	}

	public void setS(Button s) {
		S = s;
	}

	public RadioButton getCash() {
		return cash;
	}

	public void setCash(RadioButton cash) {
		this.cash = cash;
	}

	public RadioButton getStock() {
		return stock;
	}

	public void setStock(RadioButton stock) {
		this.stock = stock;
	}

	public CheckBox getAccount() {
		return account;
	}

	public void setAccount(CheckBox account) {
		this.account = account;
	}

	public TextField getUsername() {
		return username;
	}

	public void setUsername(TextField username) {
		this.username = username;
	}

	public PasswordField getPassword() {
		return password;
	}

	public void setPassword(PasswordField password) {
		this.password = password;
	}

	public HBox getUser() {
		return user;
	}

	public void setUser(HBox user) {
		this.user = user;
	}

	public HBox getPass() {
		return pass;
	}

	public void setPass(HBox pass) {
		this.pass = pass;
	}

	public ToggleGroup getGroup() {
		return group;
	}

}
