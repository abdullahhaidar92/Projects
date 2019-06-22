package view;

import javafx.scene.layout.HBox;

import javafx.scene.layout.VBox;
import javafx.scene.paint.Color;
import javafx.scene.layout.FlowPane;

import javafx.geometry.Orientation;
import javafx.geometry.Pos;
import javafx.scene.control.Button;

import javafx.scene.text.*;

public class SupplierView {

	private VBox V1 = new VBox();
	private VBox V2 = new VBox();
	private Text[] Col1 = new Text[7];
	private Text[] Col2 = new Text[7];
	private HBox H = new HBox();
	private Button Ok = new Button("OK");
	private FlowPane root = new FlowPane(Orientation.HORIZONTAL);
	private String style = "-fx-font: 25px Verdana;";

	public SupplierView() {
		root.setStyle("-fx-border-color: red;-fx-border-width: 10px;");
		root.getChildren().addAll(V1, V2);
		Col1[0] = new Text("Supplier ID");
		Col1[1] = new Text("First Name");
		Col1[2] = new Text("Last Name");
		Col1[3] = new Text("Address");
		Col1[4] = new Text("Phone");
		Col1[5] = new Text("Gender");
		Col1[6] = new Text("Company");
		V1.getChildren().addAll(Col1);
		for (int i = 0; i < 7; i++) {
			Col1[i].setStyle(style);
			Col2[i] = new Text("1111111111111111");
			Col2[i].setStyle(style);
			Col1[i].setFill(Color.RED);

		}

		V2.getChildren().addAll(Col2);
		H.getChildren().add(Ok);
		V2.getChildren().add(H);
		H.setAlignment(Pos.BOTTOM_RIGHT);
		V1.setSpacing(15);
		V2.setSpacing(15);
		root.setHgap(20);
		root.setAlignment(Pos.CENTER);
		Ok.setStyle(" -fx-font-size: 25px;-fx-body-color: red;-fx-text-fill: yellow");
		Ok.setMinWidth(40);
		Ok.setMinHeight(40);
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

	public Text[] getCol1() {
		return Col1;
	}

	public void setCol1(Text[] col1) {
		Col1 = col1;
	}

	public Text[] getCol2() {
		return Col2;
	}

	public void setCol2(Text[] col2) {
		Col2 = col2;
	}

	public HBox getH() {
		return H;
	}

	public void setH(HBox h) {
		H = h;
	}

	public Button getOk() {
		return Ok;
	}

	public void setOk(Button ok) {
		Ok = ok;
	}

	public FlowPane getRoot() {
		return root;
	}

	public void setRoot(FlowPane root) {
		this.root = root;
	}

	public String getStyle() {
		return style;
	}

	public void setStyle(String style) {
		this.style = style;
	}

}
