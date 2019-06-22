package controller;


import java.sql.SQLException;
import java.util.ArrayList;

import javafx.scene.paint.Color;

import java.lang.Double;
import model.Connecting;
import model.EmployeeModel;
import view.EmployeeView;
import javafx.scene.text.Text;
public class EmployeeController {
	protected Connecting Connection;
	protected EmployeeModel M;
	protected EmployeeView V;
	

	public EmployeeController(EmployeeModel M, EmployeeView V, Connecting con) {

		Connection = con;
		M = new EmployeeModel(Connection);
		this.M = M;
		this.V = V;
		final EmployeeModel M1 = M;
		 V.getAdd().setOnAction(e -> {
			 V.getRoot().setCenter(V.getAddEmployeePane());
		 });
		 
		 V.getSearch().setOnAction(e ->{
			 V.getB6().getChildren().removeAll(V.getText2());
			 V.getB7().getChildren().removeAll(V.getText3());
			 //V.SearchEmployeePane.getChildren().add(V.B2[0]);
			 V.getRoot().setCenter(V.getSearchEmployeePane());
		 });
		 
		 V.getA().setOnAction(e ->{
			 try {
				
		M1.AddEmployee(V.getTextfield1()[0].getText(), V.getTextfield1()[1].getText(), V.getTextfield1()[2].getText(), V.getTextfield1()[3].getText(), V.getTextfield1()[4].getText(), V.getTextfield1()[5].getText(), V.getTextfield1()[6].getText(), Double.valueOf(V.getTextfield1()[7].getText()), V.getTextfield1()[8].getText(),V.getTextfield1()[9].getText(),V.getAccount().isSelected(), V.getGroup().getSelectedToggle().getUserData().toString(),V.getUsername().getText(),V.getPassword().getText());
			} catch (NumberFormatException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			}catch (SQLException e1) {
			
				e1.printStackTrace();
			}
		 });
		 V.getAccount().setOnAction(e->{
			if(!V.getAccount().isSelected()) {
			 V.getUser().setDisable(true);
			 V.getPass().setDisable(true);
			  V.getCash().setDisable(true);
			  V.getStock().setDisable(true);
			 
			 }
			 else {
				 V.getUser().setDisable(false);
				 V.getPass().setDisable(false);
				  V.getCash().setDisable(false);
				  V.getStock().setDisable(false);
			 }
		 });
		 V.getId().setOnAction(e ->{
		     V.getB6().getChildren().removeAll(V.getText2());
			 V.getB7().getChildren().removeAll(V.getText3());
			 String[] s=new String[10];
			 String e1=V.getId().getText();
			 s=M1.getEmployee(e1);
			 if(s==null)
				 return;
			 for(int i=0;i<9;i++) {
				 V.getText3()[i].setText(s[i+1]);
			 }
			     V.getB6().getChildren().addAll(V.getText2());
				 V.getB7().getChildren().addAll(V.getText3());
		 });
		 
		 V.getStats().setOnAction(e ->{
			 V.getB9().getChildren().removeAll(V.getText5());
			 //V.SearchEmployeePane.getChildren().add(V.B2[0]);
			 String[] s=new String[6];
			 try {
				s=M1.getStats();
			} catch (SQLException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			}
		
			 if(s==null)
				 s= new String[] {"text1","text2","text3","text4","text5","text6"};
				 
			 for(int i=0;i<6;i++) {
				 V.getText5()[i].setText(s[i]);
			 }
			     
				 V.getB9().getChildren().addAll(V.getText5());
				 V.getRoot().setCenter(V.getEmployeeStats());
		 });
		 
		 V.getAll_stats().setOnAction(e -> {
			 V.getRoot().setCenter(V.getAll_Stats());
		 });
		 V.getVisit().setOnAction(e->{
			V.getB10().getChildren().clear();
			 String visitor=V.getVisitor().getText();
			 ArrayList<String> results=M1.getResults(visitor);
			 for(int i=0;i<results.size();i++) {
				
			    if(results.get(i).contains("Title:")) {
			    	
			    	Text t=new Text(results.get(i).substring(6));
			    	t.setFill(Color.RED);
			    	t.setStyle(" -fx-font: 35px Times;");
			    	 V.getB10().getChildren().add(t);
			    }
			    else {
			    	Text t=new Text(results.get(i));
				    t.setStyle(" -fx-font: 25px Verdana;");
				    V.getB10().getChildren().add(t);
			    	
			    }
			   
			 }
		
		 
		 
		 });
		 
		 
		 
		 
		 
	}

}