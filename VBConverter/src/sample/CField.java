package sample;

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
import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.time.Duration;
import java.util.Collection;
import java.util.Collections;
import java.util.regex.Matcher;
import java.util.regex.Pattern;


public class CField extends VBox{

    private static final Label nameLabel = new Label("New");
    private static final CodeArea codeArea = new CodeArea();

    public final void clear() {
        codeArea.clear();
        expandWithEmptyLines();
        setName("New");

    }
    private void setName(String name){
        nameLabel.setText(name);
    }

    private void setText(String text){
        codeArea.clear();
        codeArea.appendText(text);
        expandWithEmptyLines();
    }
    public final String getText(){
       return codeArea.getText();
    }

    public void open(String fileName,String text){
        setText(text);
        setName(fileName);
    }


    public CField() {
        setMinWidth(620);
        AnchorPane header = new AnchorPane();
        header.getStyleClass().addAll("header", "header1");
        nameLabel.getStyleClass().add("name");
        header.getChildren().add(nameLabel);
        header.setMinWidth(620);
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
        scrollPane.setMinHeight(600);
        scrollPane.setMinWidth(645);
        AnchorPane pane = new AnchorPane(scrollPane);
        pane.setMinHeight(500);
        getChildren().addAll(header, pane);
        codeArea.setStyle("-fx-font-size: 15px;");

    }


    private static StyleSpans<Collection<String>> computeHighlighting(String text) {
        Matcher matcher = PATTERN.matcher(text);
        int lastKwEnd = 0;
        StyleSpansBuilder<Collection<String>> spansBuilder
                = new StyleSpansBuilder<>();
        while (matcher.find()) {
            String styleClass =
                    matcher.group("KEYWORD") != null ? "keyword" :
                            matcher.group("PAREN") != null ? "paren" :
                                    matcher.group("BRACE") != null ? "brace" :
                                            matcher.group("BRACKET") != null ? "bracket" :
                                                    matcher.group("SEMICOLON") != null ? "semicolon" :
                                                            matcher.group("STRING") != null ? "string" :
                                                                    matcher.group("COMMENT") != null ? "comment" :
                                                                            null; /* never happens */
            assert styleClass != null;
            spansBuilder.add(Collections.emptyList(), matcher.start() - lastKwEnd);
            spansBuilder.add(Collections.singleton(styleClass), matcher.end() - matcher.start());
            lastKwEnd = matcher.end();
        }
        spansBuilder.add(Collections.emptyList(), text.length() - lastKwEnd);
        return spansBuilder.create();
    }



    private static final String[] KEYWORDS = new String[]{
            "break", "byte", "char", "const", "continue","double", "else",
            "float", "for",  "if","int","long","return", "short", "void",  "while"
    };

    private static final String KEYWORD_PATTERN = "\\b(" + String.join("|", KEYWORDS) + ")\\b";
    private static final String PAREN_PATTERN = "\\(|\\)";
    private static final String BRACE_PATTERN = "\\{|\\}";
    private static final String BRACKET_PATTERN = "\\[|\\]";
    private static final String SEMICOLON_PATTERN = "\\;";
    private static final String STRING_PATTERN = "\"([^\"\\\\]|\\\\.)*\"";
    private static final String COMMENT_PATTERN = "//[^\n]*" + "|" + "/\\*(.|\\R)*?\\*/";
    private static final Pattern PATTERN = Pattern.compile(
            "(?<KEYWORD>" + KEYWORD_PATTERN + ")"
                    + "|(?<PAREN>" + PAREN_PATTERN + ")"
                    + "|(?<BRACE>" + BRACE_PATTERN + ")"
                    + "|(?<BRACKET>" + BRACKET_PATTERN + ")"
                    + "|(?<SEMICOLON>" + SEMICOLON_PATTERN + ")"
                    + "|(?<STRING>" + STRING_PATTERN + ")"
                    + "|(?<COMMENT>" + COMMENT_PATTERN + ")"
    );





    /**
     * TODO Remove this in the future
     * A silly method to improve the look of the CodeArea
     */
    private static final void expandWithEmptyLines(){
        if(codeArea.getText().chars().filter(ch -> ch == '\n').count()<38)
         codeArea.appendText("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
    }



}







