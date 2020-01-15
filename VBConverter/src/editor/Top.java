package editor;

import javafx.scene.control.Menu;
import javafx.scene.control.MenuBar;
import javafx.scene.control.MenuItem;
import javafx.scene.control.SeparatorMenuItem;
import javafx.scene.input.Clipboard;
import javafx.scene.input.ClipboardContent;
import javafx.scene.layout.VBox;
import javafx.stage.FileChooser;

import java.io.*;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;

import com.devdaily.system.SystemCommandExecutor;

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
           clean();
        });

        Open.setOnAction(t -> {
            clean();
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
            List<String> commands = new ArrayList<String>();
            commands.add("/bin/sh");
            commands.add("-c");
            commands.add("~/Desktop/Code/gitProject/Projects/VBConverter/compiler/target.exe");

            SystemCommandExecutor commandExecutor = new SystemCommandExecutor(commands);
            boolean result = false;
            try {
                result = commandExecutor.executeCommand(cfield.getText().stripTrailing());
            } catch (IOException | InterruptedException e) {
                vbfield.error("Internal error occurred.\nTry rerunning the program.");
            }

            if (result) {
                vbfield.success(commandExecutor.getStandardOutputFromCommand().toString());
                cfield.clearErrorList();
            } else {
                final List<Integer> errorLines = new ArrayList<>();
                final String errorBuffer = commandExecutor.getStandardErrorFromCommand().toString();
                vbfield.error(errorBuffer);
                for (String line : errorBuffer.split("\n")) {
                    if (line.indexOf("line ") == 0)
                        errorLines.add(Integer.valueOf(line.split(" ")[1])-1);
                }
                cfield.showErrors(errorLines);
            }
        });

        Clear.setOnAction(t -> {
            vbfield.clear();
            cfield.clearErrorList();
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
        cfield.setName(Paths.get(fileName).getFileName().toString());
    }

    private String saveAs() {
        FileChooser fileChooser = new FileChooser();
        fileChooser.setTitle("Save File");
        return fileChooser.showSaveDialog(Main.primaryStage).getAbsoluteFile().getAbsolutePath();

    }
    private final void clean(){
        cfield.clear();
        cfield.clearErrorList();
        vbfield.clear();
        fileName=null;
    }

}
