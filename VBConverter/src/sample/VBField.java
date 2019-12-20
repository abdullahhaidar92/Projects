package sample;

import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.control.Label;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.StackPane;
import javafx.scene.layout.VBox;
import javafx.scene.paint.Color;
import javafx.scene.text.Font;
import javafx.scene.text.Text;

public class VBField extends VBox {

    private static final Label name = new Label("Output");
    private static final Text output = new Text();

    public VBField() {

        final AnchorPane header = new AnchorPane();
        final StackPane stack = new StackPane(output);
        getChildren().addAll(header, stack);
        header.getChildren().add(name);

        setMinWidth(654);
        header.getStyleClass().addAll("header", "header2");
        stack.setMinHeight(600);
        stack.setMaxWidth(654);
        stack.setAlignment(Pos.TOP_LEFT);
        stack.setPadding(new Insets(0, 0, 0, 10));
        stack.setStyle("fx-background-color:white");
        output.setFont(new Font(15));
        name.getStyleClass().add("name");

    }

    public final String getText(){
        return output.getText();
    }

    public final void success(String text) {
        output.setFill(Color.GREEN);
        output.setText(text);
    }

    public final void error(String text) {
        output.setFill(Color.RED);
        output.setText(text);
    }

    public final void clear() {
        output.setText("");
    }

}


