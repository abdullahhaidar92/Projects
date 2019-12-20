package sample;

import javafx.scene.control.Menu;
import javafx.scene.control.MenuBar;
import javafx.scene.control.MenuItem;
import javafx.scene.control.SeparatorMenuItem;
import javafx.scene.input.Clipboard;
import javafx.scene.input.ClipboardContent;
import javafx.scene.layout.VBox;
import javafx.stage.FileChooser;
import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Paths;


public class Top extends VBox {

    private static CField cfield;
    private static VBField vbfield;
    private static String fileName = null;

    public Top(MainView mainView) {

        cfield = mainView.getCField();
        vbfield = mainView.getVbField();

        MenuBar menu = new MenuBar();
        menu.getStyleClass().add("menu-bar");
        getChildren().add(menu);

        Menu file = new Menu("File");
        Menu edit = new Menu("Run");
        Menu help = new Menu("Help");
        menu.getMenus().addAll(file, edit, help);

        MenuItem New = new MenuItem("New");
        MenuItem Open = new MenuItem("Open");
        MenuItem Save = new MenuItem("Save");
        MenuItem SaveAs = new MenuItem("Save as");
        MenuItem Exit = new MenuItem("Exit");
        file.getItems().addAll(New, Open, Save, SaveAs, new SeparatorMenuItem(), Exit);

        MenuItem Convert = new MenuItem("Convert");
        MenuItem Copy = new MenuItem("Copy Output");
        MenuItem Clear = new MenuItem("Clear Output");
        MenuItem SaveOutput = new MenuItem("Save Output");
        edit.getItems().addAll(Convert, Copy, Clear, new SeparatorMenuItem(), SaveOutput);

        MenuItem About = new MenuItem("About");
        help.getItems().add(About);

        New.setOnAction(t -> {
            cfield.clear();
            fileName = null;
        });

        Open.setOnAction(t -> {
            FileChooser fileChooser = new FileChooser();
            fileChooser.setTitle("Open Resource File");
            try {
                fileName = fileChooser.showOpenDialog(Main.primaryStage).getAbsoluteFile().getAbsolutePath();
                cfield.open(Paths.get(fileName).getFileName().toString(),
                        Files.readString(Paths.get(fileName), StandardCharsets.US_ASCII));
            } catch (Exception e) {
            }

        });

        Save.setOnAction(t -> {
            try {

                if (fileName == null)
                    fileName = saveAs();
                save(fileName, cfield.getText());
            } catch (Exception e) {
            }
        });

        SaveAs.setOnAction(t -> {
            try {
                fileName = saveAs();
                save(fileName, cfield.getText());
            } catch (Exception e) {
            }
        });

        Exit.setOnAction(t -> {
            System.exit(0);
        });

        Convert.setOnAction(t -> {
            String text = "Not Failed \n Hello ";

            // Code to convert

            if (text.substring(0, 7).contains("Failed ")) {
                vbfield.error(text);
            } else {
                vbfield.success(text);
            }

        });
        Clear.setOnAction(t -> {
            vbfield.clear();
        });

        Copy.setOnAction(t -> {
            final Clipboard clipboard = Clipboard.getSystemClipboard();
            final ClipboardContent content = new ClipboardContent();
            content.putString(vbfield.getText());
            clipboard.setContent(content);
        });

        SaveOutput.setOnAction(t -> {
            try {
                save(saveAs(), vbfield.getText());
            } catch (IOException e) {
            }

        });

    }

    public final void save(String fileName, String text) throws IOException {
        Files.writeString(Paths.get(fileName), text, StandardCharsets.US_ASCII);
    }

    private String saveAs() {
        FileChooser fileChooser = new FileChooser();
        fileChooser.setTitle("Save File");
        return fileChooser.showSaveDialog(Main.primaryStage).getAbsoluteFile().getAbsolutePath();

    }
}
