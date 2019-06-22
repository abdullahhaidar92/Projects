package model;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

public class RevenueStats implements StatisticsVisitor {
     private ArrayList<String> result;
	@Override
	public void visit(Manager m) {
		Connecting connection=new Connecting();
		connection.Connect("sa","VoomBoom777Gm");
		ResultSet rs1=connection.getResults(" exec getAllRevenue");
		ResultSet rs2=connection.getResults(" exec getAverageOrders");
		ResultSet rs3=connection.getResults(" exec getNbOrders");
		try {
			if(rs1.next()&&rs2.next()&&rs3.next()) {
				result=new ArrayList<String>();
				result.add("Title: Daily Revenue : ");
				result.add("total revenue for this day:  "+rs1.getDouble("daily")+"  "+rs1.getString("currancy"));
				result.add("average revenue for this day:  "+rs2.getDouble("daily")+"  "+rs1.getString("currancy"));
				result.add("total nb of orders for this day:  "+rs3.getInt("daily")+"  "+" orders ");
				result.add("Title: Monthly Revenue : ");
				result.add("total revenue for this month:  "+rs1.getDouble("monthly")+"  "+rs1.getString("currancy"));
				result.add("average revenue for this month:  "+rs2.getDouble("monthly")+"  "+rs1.getString("currancy"));
				result.add("total nb of orders for this month:  "+rs3.getInt("monthly")+"  "+" orders ");
				result.add("Title: Yearly Revenue : ");
				result.add("total revenue for this year:  "+rs1.getDouble("yearly")+"  "+rs1.getString("currancy"));
				result.add("average revenue for this year:  "+rs2.getDouble("yearly")+"  "+rs1.getString("currancy"));
				result.add("total nb of orders for this year:  "+rs3.getInt("yearly")+"  "+" orders ");
				
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	public ArrayList<String> getResult() {
		return result;
	}
	public void setResult(ArrayList<String> result) {
		this.result = result;
	}

}
