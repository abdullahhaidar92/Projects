package editor;


import javafx.scene.Node;
import javafx.scene.control.Label;
import javafx.scene.layout.HBox;
import org.fxmisc.richtext.LineNumberFactory;
import org.fxmisc.richtext.model.StyleSpans;
import org.fxmisc.richtext.model.StyleSpansBuilder;

import java.util.Collection;
import java.util.Collections;
import java.util.function.IntFunction;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class VBField extends CodeField {

    public VBField() {
        setName("Output");
        setHeaderStyle("header2");
        IntFunction<Node> numberFactory = LineNumberFactory.get(codeArea);
        IntFunction<Node> graphicFactory = line -> {
            HBox hbox = new HBox();
            Label label=new Label();
            label.setMinHeight(20);
            label.setText(" ");
            hbox.getChildren().add(label);
            return hbox;
        };

        codeArea.setParagraphGraphicFactory(graphicFactory);
    }

    public final void success(String text) {
       setText(text);
    }

    public final void error(String text) {
        setText(text);
    }
    @Override
    protected StyleSpans<Collection<String>> computeHighlighting(String text) {
        Pattern pattern=Pattern.compile(
                "(?<KEYWORD>" + "\\b(" + "End|Character|continue|Double|Else|Sngle|For|If|Integer|Dim|Next|As|to|And|Or|Not" + ")\\b" + ")"
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


