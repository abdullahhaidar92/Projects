package editor;

import javafx.scene.control.*;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.BorderPane;
import javafx.scene.layout.HBox;



public class MainView extends BorderPane {

    private static final CField cfield=new CField();
    private static final VBField vbfield=new VBField();

    public MainView(){

        final Top top=new Top(this);
        final HBox fields=new HBox();
        final AnchorPane bottom=new AnchorPane();

        setTop(top);
        setCenter(fields);
        setBottom(bottom);

        fields.getChildren().addAll(cfield,vbfield);
        bottom.getStyleClass().add("footer");
        bottom.getChildren().add(new Label("  Made By Abdullah Haidar" ));

        setId("root");
        fields.getStyleClass().add("hbox");
    }

    public CField getCField() {
        return cfield;
    }

    public VBField getVbField() {
        return vbfield;
    }




}
