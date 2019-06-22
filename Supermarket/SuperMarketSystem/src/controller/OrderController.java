package controller;
import java.sql.ResultSet;
import java.sql.SQLException;
import javafx.stage.Stage;
import model.Connecting;
import model.OrderEntry;
import model.OrderModel;
import view.Home;
import view.OrdersView;

public class OrderController {
	protected Stage Supplier;
	protected Connecting Connection;
	protected OrderModel M;
	protected 	OrdersView V = new OrdersView();

	public OrderController(OrderModel M, OrdersView V, Connecting con,Home View) {

		Connection = con;
		M = new OrderModel(Connection);
		this.M = M;
		this.V = V;
		final OrderModel M1 = M;
		V.getSidebuttons()[0].setOnAction(e -> {
			
			V.getOrderPane().setCenter(V.getInnerPages()[0]);

			V.getOrders().setItems(M1.data);
			M1.getOrders();
			V.getInnerPages()[0].getChildren().remove(V.getBottom1());
			V.getInnerPages()[0].getChildren().remove(V.getBottom2());

		});
		V.getSidebuttons()[1].setOnAction(e -> {
			// System.out.println("Hello");
			V.getOrderPane().setCenter(V.getInnerPages()[1]);
			V.getInfo()[1].setText("Customer Id");
			V.getInfo()[2].setText("Employee Id");
			V.getInfo()[3].setText("Date");
			V.getInfo()[4].setText("Total");
			V.getInfo()[5].setText("Currancy");
			V.getInfo()[11].setText("Contents");
			V.getOrder().setVisible(true);
			
			// V.Orders.setItems(M1.data);
			// M1.getOrders();;

		});
		
		
		V.getSidebuttons()[2].setOnAction(e -> {
			V.getInnerPages()[0].getChildren().remove(V.getBottom1());
			V.getInnerPages()[0].getChildren().remove(V.getBottom2());
			M1.data.removeAll(M1.data);
			V.getOrderPane().setCenter(V.getInnerPages()[0]);
			V.getInnerPages()[0].getChildren().remove(V.getBottom2());
			V.getInnerPages()[0].getChildren().add(V.getBottom1());
		});
		V.getSidebuttons()[3].setOnAction(e -> {
			V.getInnerPages()[0].getChildren().remove(V.getBottom1());
			V.getInnerPages()[0].getChildren().remove(V.getBottom2());
			M1.data.removeAll(M1.data);
			V.getOrderPane().setCenter(V.getInnerPages()[0]);
			V.getInnerPages()[0].getChildren().remove(V.getBottom1());
			V.getInnerPages()[0].getChildren().add(V.getBottom2());
		});
		V.getOrderId().setOnAction(e -> {
			// System.out.println("Hello");
			ResultSet rs = M1.getOrderInfo(V.getOrderId().getText());
			try {
				V.getInfo()[6].setText(rs.getString("CID"));
				V.getInfo()[7].setText(rs.getString("EId"));
				V.getInfo()[8].setText(rs.getDate("Date").toString());
				V.getInfo()[9].setText("" + rs.getDouble("Total"));
				V.getInfo()[10].setText(rs.getString("Total_Currancy"));
				V.getOrder().setItems(M1.data1);
				M1.data1.removeAll(M1.data1);
				rs = M1.getOrderDetails(V.getOrderId().getText());
				while (rs.next())
					M1.data1.add(new OrderEntry("", rs.getString("PNAME"), rs.getInt("Quantity"),
							"", rs.getDouble("PRICE"), rs.getString("PRICE_Currancy")));
				// V.Orders.setItems(M1.data);
				// M1.getOrders();;
			} catch (SQLException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			}

		});
		
		V.getEmployeeId().setOnAction(e->{
			M1.getEmployeeOrders(V.getEmployeeId().getText());
			V.getOrders().setItems(M1.data);
			
		});
		V.getDate2().setOnAction(e->{
			
			try {
				M1.getDateOrders(V.getDate1().getText(),V.getDate2().getText());
				V.getOrders().setItems(M1.data);
			} catch (SQLException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			}
			
		});
		V.getSidebuttons()[4].setOnAction(e->{
			View.getRoot().setCenter(View.getHome_center());
		});
	}
}