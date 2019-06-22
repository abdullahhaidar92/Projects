package application;
	
import controller.Controller;
import javafx.application.Application;
import javafx.stage.Stage;
import javafx.stage.StageStyle;
import model.Model;
import view.Home;
import javafx.scene.Scene;
public class Main extends Application {
			String type;
	        Stage S;
	    	
	@Override
	public void start(Stage primaryStage) {
		try {
			
			 Model M=new Model();
			 Home V=new Home();
			 primaryStage.initStyle(StageStyle.UNDECORATED);
			 Controller C=new Controller(M,V,primaryStage);
			Scene scene = new Scene(V.getRoot(),1365,730);
			scene.getStylesheets().add(getClass().getResource("/css/application.css").toExternalForm());
			primaryStage.setScene(scene);
			primaryStage.show();
			primaryStage.setTitle("Calculator");
			C.showPopUp();
			
		} catch(Exception e) {
			e.printStackTrace();
		}
	}
	
	public static void main(String[] args) {
		launch(args);
	}
}
