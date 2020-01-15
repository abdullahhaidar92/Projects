package editor;

import javafx.geometry.Pos;
import javafx.scene.Node;
import javafx.scene.layout.HBox;
import javafx.scene.paint.Color;
import javafx.scene.shape.Polygon;
import org.fxmisc.richtext.model.StyleSpans;
import org.fxmisc.richtext.model.StyleSpansBuilder;

import java.util.*;
import java.util.function.IntFunction;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class CField extends CodeField {

    public CField(){
        setHeaderStyle("header1");
    }

    @Override
    public  void clear() {
        super.clear();
        setName("New");
        clearErrorList();
    }

    public void open(String fileName, String text) {
        setName(fileName);
        setText(text);
    }

    public void clearErrorList() {
        showErrors(new LinkedList<>());
    }

    public void showErrors(List<Integer> errors) {
        IntFunction<Node> numberFactory = getLineNumberFactory();
        IntFunction<Node> arrowFactory = new IntFunction<>() {
            final List<Integer> shownLines = errors;

            public Node apply(int lineNumber) {
                Polygon triangle = new Polygon(0.0, 0.0, 10.0, 5.0, 0.0, 10.0);
                if (shownLines.contains(lineNumber))
                    triangle.setFill(Color.RED);
                else
                    triangle.setFill(Color.TRANSPARENT);
                return triangle;
            }
        };

        IntFunction<Node> graphicFactory = line -> {
            HBox hbox = new HBox(numberFactory.apply(line),
                    arrowFactory.apply(line));
            hbox.setAlignment(Pos.CENTER_LEFT);
            return hbox;
        };

        setParagraphGraphicFactory(graphicFactory);

    }

    @Override
    protected StyleSpans<Collection<String>> computeHighlighting(String text) {
        Pattern pattern=Pattern.compile(
                "(?<KEYWORD>" + "\\b(" + "break|char|while|continue|double|else|float|for|if|int|long|return|void" + ")\\b" + ")"
                        + "|(?<PAREN>" + "\\(|\\)" + ")"
                        + "|(?<BRACE>" + "\\{|\\}" + ")"
                        + "|(?<BRACKET>" + "\\[|\\]" + ")"
                        + "|(?<SEMICOLON>" + "\\;" + ")"
                        + "|(?<STRING>" + "\"([^\"\\\\]|\\\\.)*\"" + ")"
                        + "|(?<COMMENT>" + "//[^\n]*" + "|" + "/\\*(.|\\R)*?\\*/" + ")"
        );
        Matcher matcher = pattern.matcher(text);
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

}







