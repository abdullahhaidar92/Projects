package view;

import javafx.scene.image.ImageView;
import javafx.scene.layout.VBox;

import javafx.scene.layout.FlowPane;

import javafx.geometry.Insets;

import javafx.geometry.Orientation;
import javafx.geometry.Pos;
import javafx.scene.control.Button;
import javafx.scene.control.TextField;

import javafx.scene.text.*;

public class ProductView {
	private FlowPane ProductPane;
	private Text [] info;
	private   TextField []inputs;
	private ImageView img=new ImageView();
	private Button add=new Button("Add Product"),preview=new Button("Add Image");
	private  TextField image=new TextField();
	public ProductView() {
		String styleText = "-fx-font: 35px Verdana;";
		String styleButton = " -fx-font-size: 45px;-fx-body-color: pink;";
		String style2 = " -fx-font: 25px Verdana;-fx-background-color: cyan;";
	
	
			
   


		ProductPane =  new FlowPane(Orientation.HORIZONTAL);
		ProductPane.setPadding(new Insets(1,20,10,40));

		
		VBox V1=new VBox();
		VBox V2=new VBox();
		VBox V3=new VBox();
		VBox V4=new VBox();
		info=new Text[8];
		info[0]=new Text("Product Id");
		info[1]=new Text("Name");
		info[2]=new Text("Category");
		info[3]=new Text("Price");
		info[4]=new Text("Price Currancy");
		info[5]=new Text("Expiray Date");
		info[6]=new Text("TVA Ratio");
		info[7]=new Text("Barcode");
		for(int i=0;i<8;i++) 
		info[i].setStyle(styleText);
		
		V1.getChildren().addAll(info);
		V1.setSpacing(20);
		V2.setSpacing(12.5);
		V3.setSpacing(20);
		inputs=new TextField[8];
		for(int i=0;i<8;i++) {
			inputs[i]=new TextField();
			inputs[i].setStyle(style2);
			inputs[i].setMinWidth(400);
		}
		V2.getChildren().addAll(inputs);
		//	V1.setPadding(new Insets(10,50,10,10));
		ProductPane.getChildren().addAll(V1,V2,V4);
		ProductPane.setHgap(120);
        V3.getChildren().addAll(img,image,preview);
        V3.setAlignment(Pos.BOTTOM_CENTER);
        V3.setMinHeight(300);
        V4.setSpacing(100);
        V4.setAlignment(Pos.BOTTOM_CENTER);
        V4.getChildren().addAll(V3,add);
        V4.setPadding(new Insets(1,1,20,1));
        add.setStyle(styleButton);
	    
		

	}
	public FlowPane getProductPane() {
		return ProductPane;
	}
	public void setProductPane(FlowPane productPane) {
		ProductPane = productPane;
	}
	public Text[] getInfo() {
		return info;
	}
	public void setInfo(Text[] info) {
		this.info = info;
	}
	public TextField[] getInputs() {
		return inputs;
	}
	public void setInputs(TextField[] inputs) {
		this.inputs = inputs;
	}
	public ImageView getImg() {
		return img;
	}
	public void setImg(ImageView img) {
		this.img = img;
	}
	public Button getAdd() {
		return add;
	}
	public void setAdd(Button add) {
		this.add = add;
	}
	public Button getPreview() {
		return preview;
	}
	public void setPreview(Button preview) {
		this.preview = preview;
	}
	public TextField getImage() {
		return image;
	}
	public void setImage(TextField image) {
		this.image = image;
	}
	
	
	

}
