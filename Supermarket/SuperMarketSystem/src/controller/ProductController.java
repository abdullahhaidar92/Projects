package controller;


import java.sql.SQLException;

import javafx.scene.image.Image;

import java.lang.Double;
import model.Connecting;
import model.ProductModel;
import view.ProductView;

public class ProductController {
	protected Connecting Connection;
	protected ProductModel M;
	protected ProductView V;
	protected String imageUrl = "file:///C:/Users/hp/Desktop/SuperMarketProducts/";

	public ProductController(ProductModel M, ProductView V, Connecting con) {

		Connection = con;
		M = new ProductModel(Connection);
		this.M = M;
		this.V = V;
		final ProductModel M1 = M;

		V.getAdd().setOnAction(e -> {
			try {
				M1.addProduct(V.getInputs()[0].getText(),V.getInputs()[1].getText(),V.getInputs()[2].getText(),Double.valueOf(V.getInputs()[3].getText()),
						V.getInputs()[4].getText(),V.getInputs()[5].getText(),Double.valueOf(V.getInputs()[6].getText()),V.getInputs()[7].getText(),V.getImage().getText());
			} catch (NumberFormatException | SQLException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			}

		});

		V.getPreview().setOnAction(e -> {
			V.getImg().setImage(new Image(imageUrl+V.getImage().getText()));
		});

	}
}