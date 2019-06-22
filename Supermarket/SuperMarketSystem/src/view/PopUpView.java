package view;

import javafx.geometry.Orientation;
import javafx.geometry.Pos;
import javafx.scene.control.Button;
import javafx.scene.control.TextField;
import javafx.scene.control.PasswordField;
import javafx.scene.layout.FlowPane;
import javafx.scene.layout.HBox;
import javafx.scene.text.Text;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;

public class PopUpView {
	private Text user,password,note;
	private TextField userField;
	private PasswordField passField;
	private Button login;
	private  FlowPane root;
    public PopUpView(){
    	String styleText = "-fx-font: 20px Verdana;";
      	String styleText1 = "-fx-font: 15px Verdana;";
		String styleButton = " -fx-font-size: 35px;-fx-body-color: red;";
		String style2 = " -fx-font: 20px Verdana;";
		root=new FlowPane(Orientation.VERTICAL);
		user=new Text("Username");
		password=new Text("password");
		userField=new TextField("E100");
		passField=new PasswordField();
		passField.setText("VoomBoom777Gm");
        login=new Button("Log In");
        note=new Text("Please login with your cashier id and password");
        note.setStyle(styleText1);
        HBox img=new HBox();
		HBox H=new HBox();
		HBox H1=new HBox();
		HBox H2=new HBox();
		ImageView sup=new ImageView(new Image("file:///C:/Users/hp/Desktop/SuperMarketProducts/super.png"));
		img.getChildren().add(sup);
		H.getChildren().addAll(user,userField);
		H1.getChildren().addAll(password,passField);
		H2.getChildren().add(login);
		
	root.getChildren().addAll(img,note,H,H1,H2);
	H.setSpacing(10);
	H1.setSpacing(10);
	H2.setAlignment(Pos.CENTER);
	img.setAlignment(Pos.BOTTOM_CENTER);
	H.setAlignment(Pos.CENTER);
	H1.setAlignment(Pos.CENTER);
	user.setStyle(styleText);
	password.setStyle(styleText);
	userField.setStyle(style2);
	passField.setStyle(style2);
    login.setStyle(styleButton);
    root.setVgap(20);
    root.setAlignment(Pos.CENTER);
    root.setStyle("-fx-background-color:white");
	
	
	
    }
	public Text getUser() {
		return user;
	}
	public void setUser(Text user) {
		this.user = user;
	}
	public Text getPassword() {
		return password;
	}
	public void setPassword(Text password) {
		this.password = password;
	}
	public Text getNote() {
		return note;
	}
	public void setNote(Text note) {
		this.note = note;
	}
	public TextField getUserField() {
		return userField;
	}
	public void setUserField(TextField userField) {
		this.userField = userField;
	}
	public PasswordField getPassField() {
		return passField;
	}
	public void setPassField(PasswordField passField) {
		this.passField = passField;
	}
	public Button getLogin() {
		return login;
	}
	public void setLogin(Button login) {
		this.login = login;
	}
	public FlowPane getRoot() {
		return root;
	}
	public void setRoot(FlowPane root) {
		this.root = root;
	}
    
    
    
    
    
    
}
