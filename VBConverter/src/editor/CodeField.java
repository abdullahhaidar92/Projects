package editor;

import javafx.application.Platform;
import javafx.scene.control.Label;
import javafx.scene.input.KeyCode;
import javafx.scene.input.KeyEvent;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.VBox;
import org.fxmisc.flowless.VirtualizedScrollPane;
import org.fxmisc.richtext.CodeArea;
import org.fxmisc.richtext.LineNumberFactory;
import org.fxmisc.richtext.model.StyleSpans;
import org.fxmisc.richtext.model.StyleSpansBuilder;
import org.reactfx.Subscription;

import java.time.Duration;
import java.util.Collection;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class CodeField extends VBox {

    protected  Label nameLabel = new Label("New");
    protected  CodeArea codeArea = new CodeArea();
    private AnchorPane header = new AnchorPane();
    private double minWidth=650;
    private double minHeight=600;

    public void clear() {
        codeArea.clear();
        expandWithEmptyLines();
    }

    protected void setName(String name) {
        nameLabel.setText(name);
    }

    protected void setText(String text) {
        codeArea.clear();
        codeArea.appendText(text);
        expandWithEmptyLines();
    }

    public final String getText() {
        return codeArea.getText();
    }

    public CodeField() {
        setMinWidth(minWidth);
        header.getStyleClass().add("header");
        nameLabel.getStyleClass().add("name");
        header.getChildren().add(nameLabel);
        header.setMinWidth(minWidth);
        codeArea.setParagraphGraphicFactory(LineNumberFactory.get(codeArea));
        Subscription cleanupWhenNoLongerNeedIt = codeArea.multiPlainChanges()
                .successionEnds(Duration.ofMillis(100))
                .subscribe(ignore -> codeArea.setStyleSpans(0, computeHighlighting(codeArea.getText())));
        final Pattern whiteSpace = Pattern.compile("^\\s+");
        codeArea.addEventHandler(KeyEvent.KEY_PRESSED, KE ->
        {
            if (KE.getCode() == KeyCode.ENTER) {
                int caretPosition = codeArea.getCaretPosition();
                int currentParagraph = codeArea.getCurrentParagraph();
                Matcher m0 = whiteSpace.matcher(codeArea.getParagraph(currentParagraph - 1).getSegments().get(0));
                if (m0.find()) Platform.runLater(() -> codeArea.insertText(caretPosition, m0.group()));
            }
        });

        clear();
        VirtualizedScrollPane scrollPane = new VirtualizedScrollPane<>(codeArea);
        scrollPane.setMinHeight(minHeight);
        scrollPane.setMinWidth(minWidth);
        AnchorPane pane = new AnchorPane(scrollPane);
        pane.setMinHeight(minHeight);
        getChildren().addAll(header, pane);
        codeArea.setStyle("-fx-font-size: 15px;");


    }

    public void setHeaderStyle(String headerStyleClass){
        header.getStyleClass().add(headerStyleClass);

    }

    protected StyleSpans<Collection<String>> computeHighlighting(String text) {
        StyleSpansBuilder<Collection<String>> spansBuilder
                = new StyleSpansBuilder<>();
        return spansBuilder.create();
    }

    /**
     * TODO Remove this in the future
     * A silly method to improve the look of the CodeArea
     */
    private final void expandWithEmptyLines() {
        if (codeArea.getText().chars().filter(ch -> ch == '\n').count() < 38)
            codeArea.appendText("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
    }



}
