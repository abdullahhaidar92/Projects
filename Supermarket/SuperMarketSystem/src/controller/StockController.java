package controller;

import javafx.scene.Scene;
import java.lang.Integer;
import java.sql.SQLException;
import javafx.stage.Stage;
import javafx.stage.StageStyle;
import model.Connecting;
import model.StockEntry;
import model.StockModel;
import view.StockView;
import view.SupplierView;

public class StockController {
	protected Stage Supplier;
	protected Connecting Connection;
	protected StockModel M;
	protected StockView V = new StockView();
	protected final StockModel M1;

	public StockController(StockModel M, StockView V, Connecting con) {

		Connection = con;
		M = new StockModel(Connection);
		this.M = M;
		this.V = V;
		M1 = M;
		V.getSidebuttons()[0].setOnAction(e -> {
			// System.out.println("Hello");
			V.getStockPane().setCenter(V.getInnerPages()[0]);

			V.getStocks().setItems(M1.data);
			M1.getStocks();
			for (int i = 1; i < M1.data.size() + 1; i++) {
				V.getSuppliers()[i].setVisible(true);
			}
			for (int i = M1.data.size() + 1; i < V.getSuppliers().length; i++) {
				V.getSuppliers()[i].setVisible(false);
			}

		});
		V.getSidebuttons()[1].setOnAction(e -> {
			// System.out.println("Hello");
			V.getStockPane().setCenter(V.getInnerPages()[0]);

			V.getStocks().setItems(M1.data);
			M1.getEmptyStocks();
			for (int i = 1; i < M1.data.size() + 1; i++) {
				V.getSuppliers()[i].setVisible(true);
			}
			for (int i = M1.data.size() + 1; i < V.getSuppliers().length; i++) {
				V.getSuppliers()[i].setVisible(false);
			}

		});
		V.getSidebuttons()[2].setOnAction(e -> {
			// System.out.println("Hello");
			V.getStockPane().setCenter(V.getInnerPages()[1]);

		});
		for (int i = 1; i < V.getSuppliers().length; i++) {
			final int I = i;

			this.V.getSuppliers()[I].setOnAction(e -> {

				try {
					// ShowSuppliers("SP100");

					ShowSuppliers(M1.data.get(I - 1).getSupId());
				} catch (SQLException e1) {
					// TODO Auto-generated catch block
					e1.printStackTrace();
				}

			});
		}
		V.getADD().setOnAction(e -> {
			try {
				M1.AddStock(V.getInput()[0].getText(), V.getInput()[1].getText(),
						Integer.valueOf(V.getInput()[2].getText()), Integer.valueOf(V.getInput()[3].getText()),
						Integer.valueOf(V.getInput()[4].getText()));
			} catch (NumberFormatException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			} catch (SQLException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			}

		});
		V.getSidebuttons()[3].setOnAction(e -> {
			// System.out.println("Hello");
			V.getStockPane().setCenter(V.getInnerPages()[2]);

		});
		V.getSidebuttons()[4].setOnAction(e -> {

			V.getStockPane().setCenter(V.getInnerPages()[3]);

		});

		V.getAddToStock().setOnAction(e -> {
			try {

				M1.AddToStock(V.getStockid().getText(), Integer.valueOf(V.getStockq().getText()));

			} catch (NumberFormatException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			} catch (SQLException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			}

		});
		V.getStockid2().setOnAction(e -> {
			V.getInfo()[1].setText("Product ");
			V.getInfo()[2].setText("Currant Quantity");
			V.getInfo()[3].setText("Minimum Quantity");
			V.getInfo()[4].setText("Maximum Quantity");
			StockEntry SE = M1.getStock(V.getStockid2().getText());
			V.getInfo()[5].setText("" + SE.getName());
			V.getInfo()[6].setText("" + SE.getQuantity());
			V.getInfo()[7].setText("" + SE.getMinQuantity());
			V.getInfo()[8].setText("" + SE.getMaxQuantity());

		});

	}

	public void ShowSuppliers(String SP) throws SQLException {
		Supplier = new Stage();
		Supplier.initStyle(StageStyle.UNDECORATED);
		SupplierView V = new SupplierView();
		Supplier.setScene(new Scene(V.getRoot(), 500, 500));
		Supplier.show();
		String[] SupplierInfo = M.getSupplierInfo(SP);
		V.getCol2()[0].setText(SupplierInfo[0]);
		V.getCol2()[1].setText(SupplierInfo[1]);
		V.getCol2()[2].setText(SupplierInfo[2]);
		V.getCol2()[3].setText(SupplierInfo[3]);
		V.getCol2()[4].setText(SupplierInfo[4]);
		V.getCol2()[5].setText(SupplierInfo[5].trim());
		V.getCol2()[6].setText(SupplierInfo[6].trim());
		V.getOk().setOnAction(e -> {
			Supplier.close();
		});

	}

}
