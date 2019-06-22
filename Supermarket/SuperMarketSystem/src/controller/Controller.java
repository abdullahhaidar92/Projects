package controller;

import java.sql.ResultSet;
import java.sql.SQLException;

import model.EmployeeModel;
import model.Model;
import model.OrderModel;
import model.ProductModel;
import model.StockModel;

import javafx.scene.Scene;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;

import javafx.scene.layout.FlowPane;
import javafx.stage.Modality;
import javafx.stage.Stage;
import javafx.stage.StageStyle;
import view.BillView;
import view.EmployeeView;
import view.Home;
import view.OrdersView;
import view.PopUpView;
import view.ProductView;
import view.StockView;

public class Controller {
	protected Stage PopUp, primaryStage, Bill;
	protected PopUpView PopView;
	protected BillView bill;
	protected Home V;
	protected Model M;
	protected String id, pass, type;

	public Controller(Model m, Home v, Stage primaryStage) throws SQLException {
		/////////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////// PopView and BillView
		///////////////////////////////////////////////////////////////////////////////////////// //////////////////////////////////////
		//////////////////////////////////////////////////////////////////////////////////////////
		PopView = new PopUpView();
		bill = new BillView();
		M = m;
		V = v;
		PopUp = new Stage();
		Bill = new Stage();
		FlowPane root = PopView.getRoot();
		FlowPane billroot = bill.getRoot();
		Scene scene = new Scene(root, 400, 400);
		Scene billscene = new Scene(billroot, 530, 700);
		billscene.getStylesheets().add(getClass().getResource("/css/applicationbill.css").toExternalForm());
		PopUp.setScene(scene);

		Bill.setScene(billscene);
		Bill.initStyle(StageStyle.UNDECORATED);
		PopUp.initModality(Modality.APPLICATION_MODAL);
		Bill.initModality(Modality.APPLICATION_MODAL);
		V.getOrder().setItems(M.data);

		V.getTop()[0].setOnAction(e -> {
			// trigger on expiry date
			if (M.getType().trim().equals("StockManager")) {
				PopView.getNote().setText("Please login with your cashier id and password");
				showPopUp();
			}
			if (!M.getType().trim().equals("StockManager"))
				V.getRoot().setCenter(V.getHome_center());
		});
		V.getAdd().setOnAction(e -> {
			try {
				M.addToTable(V.getBarcode().getText(), Integer.valueOf(V.getQuantity().getText()));
			} catch (NumberFormatException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			} catch (SQLException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			}
		});
		///////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////// Generate Bill
		/////////////////////////////////////////////////////////////////////////////////////////// ///////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////
		V.getGenerateBill().setOnAction(e -> {
			try {
				int OID = M.GenerateBill(V.getCustomer().getText());
				showBill();
				ResultSet rs = M.getBill(OID);
				if (rs.next()) {

					bill.getCustomerId()
							.setText(rs.getString("CusFName").trim() + " " + rs.getString("CusLName").trim());
					bill.getEmployeeId()
							.setText(rs.getString("EmpFName").trim() + " " + rs.getString("EmpLName").trim());
					bill.getOrderId().setText("" + rs.getInt("OID"));
					bill.getDate().setText(rs.getDate("DATE").toString());
					bill.getOrder().setItems(M.data);
					bill.getTotal().setText("" + rs.getDouble("TOTAL") + " " + rs.getString("TOTAL_Currancy").trim());
					bill.getTva().setText(M.getTva() + " " + rs.getString("TOTAL_Currancy"));
					M.setTva(0);

				}
			} catch (NumberFormatException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			} catch (SQLException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			}

		});
		PopView.getLogin().setOnAction(e -> {
			id = PopView.getUserField().getText();
			pass = PopView.getPassField().getText();
			try {
				if (M.VerifyConnection(id, pass)) {
					if (!M.getType().trim().equals("MASTER")) {
						if (M.getType().trim().equals("StockManager")) {
							StockModel SM = new StockModel(M.getConnection());
							StockView SV = new StockView();
							StockController S = new StockController(SM, SV, M.getConnection());
							SM.getStocks();
							S.M1.getStocks();
							V.getRoot().setCenter(SV.getStockPane());
							SV.getStockPane().setCenter(SV.getInnerPages()[0]);

							SV.getStocks().setItems(SM.data);

							for (int i = 1; i < SM.data.size() + 1; i++) {
								SV.getSuppliers()[i].setVisible(true);
							}
							for (int i = SM.data.size() + 1; i < SV.getSuppliers().length; i++) {
								SV.getSuppliers()[i].setVisible(false);
							}
							PopUp.close();
							return;
						} else
							primaryStage.setTitle(M.getType().trim() + "  " + M.getFullName().trim());
					} else if (M.getType().equals("MASTER"))
						primaryStage.setTitle("System Administration");
					PopUp.close();
					setImagesToButtons();

				}
			} catch (Exception e1) {
			//	System.out.println("Hello");
				PopUp.close();
				// TODO Auto-generated catch block
				e1.printStackTrace();
			}

			PopUp.close();
		});

		bill.getPrint().setOnAction(e -> {
			Bill.close();
			M.data.removeAll(M.data);
			V.getTotal().setText("");

		});

		V.getTop()[2].setOnAction(e -> {
			if (M.getType().trim().equals("Cashier")) {
				PopView.getNote().setText("Please login with your stockmanager id and password");
				PopUp.showAndWait();
			}
			if (M.getType().trim().equals("Cashier"))
				return;
			StockModel SM = new StockModel(M.getConnection());
			StockView SV = new StockView();
			StockController S = new StockController(SM, SV, M.getConnection());
			SM.getStocks();
			S.M1.getStocks();
			V.getRoot().setCenter(SV.getStockPane());
			SV.getStockPane().setCenter(SV.getInnerPages()[0]);

			SV.getStocks().setItems(SM.data);

			for (int i = 1; i < SM.data.size() + 1; i++) {
				SV.getSuppliers()[i].setVisible(true);
			}
			for (int i = SM.data.size() + 1; i < SV.getSuppliers().length; i++) {
				SV.getSuppliers()[i].setVisible(false);
			}

		});
		V.getTop()[1].setOnAction(e -> {
			if (M.getType().trim().equals("Cashier") || M.getType().trim().equals("StockManager")) {
				PopView.getNote().setText("Please enter the adminstrator password");
				PopView.getUserField().setText("sa");
				PopView.getPassField().setText("VoomBoom777Gm");
				PopView.getUserField().setEditable(false);
				PopUp.showAndWait();

			}
			PopView.getUserField().setEditable(true);
			if (!M.getType().trim().equals("MASTER"))
				return;
			OrderModel OM = new OrderModel(M.getConnection());
			OrdersView OV = new OrdersView();
			new OrderController(OM, OV, M.getConnection(), V);
			V.getRoot().setCenter(OV.getOrderPane());
			OV.getOrderPane().setCenter(OV.getInnerPages()[0]);

			OV.getOrders().setItems(OM.data);
			OM.getOrders();
			OV.getInnerPages()[0].getChildren().remove(OV.getBottom1());
			OV.getInnerPages()[0].getChildren().remove(OV.getBottom2());

		});
		V.getTop()[3].setOnAction(e -> {
			if (M.getType().trim().equals("Cashier")) {
				PopView.getNote().setText("Please login with your stockmanager id and password");
				PopUp.showAndWait();
			}
			if (M.getType().trim().equals("Cashier"))
				return;
			ProductModel PM = new ProductModel(M.getConnection());
			ProductView PV = new ProductView();
			new ProductController(PM, PV, M.getConnection());
			V.getRoot().setCenter(PV.getProductPane());

		});
		V.getCancel().setOnAction(e -> {
			M.data.removeAll(M.data);
			V.getTotal().setText("");

		});
		V.getTop()[4].setOnAction(e -> {

			if (M.getType().trim().equals("Cashier") || M.getType().trim().equals("StockManager")) {
				PopView.getNote().setText("Please enter the adminstrator password");
				PopView.getUserField().setText("sa");
				PopView.getPassField().setText("VoomBoom777Gm");
				PopView.getUserField().setEditable(false);
				PopUp.showAndWait();

			}
			PopView.getUserField().setEditable(true);
			if (!M.getType().trim().equals("MASTER"))
				return;
			EmployeeModel OM = new EmployeeModel(M.getConnection());
			EmployeeView OV = new EmployeeView();
			new EmployeeController(OM, OV, M.getConnection());
			V.getRoot().setCenter(OV.getRoot());

		});

		V.getTop()[5].setOnAction(e -> {
			System.exit(0);

		});
		V.getSomething().setOnAction(e -> {
			M.data.remove(V.getOrder().getSelectionModel().getSelectedItem());
			v.getTotal().setText("" + ((Double.valueOf(V.getTotal().getText())
					- V.getOrder().getSelectionModel().getSelectedItem().getUnit_Price())));

		});

	}

	public void showPopUp() {

		PopView.getUserField().setText("sa");
		PopView.getPassField().setText("VoomBoom777Gm");
		PopUp.showAndWait();
	}

	public void showBill() {
		Bill.show();
	}

	public void setImagesToButtons() throws SQLException {
		String imageUrl = "file:///C:/Users/hp/Desktop/SuperMarketProducts/";
		int N = M.fillImageArray();
		for (int i = 0; i < N; i++) {
			V.getImages()[i].setGraphic(new ImageView(new Image(imageUrl + M.getImageArray()[i][1])));
			final String PID = M.getImageArray()[i][0];

			V.getImages()[i].setOnAction(e -> {
				double total;
				if (V.getTotal().getText().equals(""))
					total = 0;
				else
					total = Double.valueOf(V.getTotal().getText());
				try {
					if (V.getQuantity().getText().equals(""))
						V.getTotal().setText("" + ((Double) (total + M.addImageToTable(PID, 1))).floatValue());
					else
						V.getTotal()
								.setText("" + ((Double) (total
										+ M.addImageToTable(PID, Integer.valueOf(V.getQuantity().getText()))))
												.floatValue());

				} catch (NumberFormatException e1) {
					// TODO Auto-generated catch block
					e1.printStackTrace();
				} catch (SQLException e1) {
					// TODO Auto-generated catch block
					e1.printStackTrace();
				}
			});

		}
	}

}
