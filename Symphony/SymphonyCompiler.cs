namespace Symphony;

public class SymphonyCompiler : Compiler
{
    public Key IDENTITYKEY = "identity";
    public Key COMPILER = "compiler";
    public Key CONTEXTUAL = "contextual";
    public Key KEY = "key";
    public Key OPENBRACES = "{";
    public Key CLOSEBRACES = "}";
    public Key EQUAL = "=";

    public Key EXPRESSION = "/.*?/";
    public Key IDENTITY = "[A-Za-z_][A-Za-z_0-9]*";

    public Rule id;
    public Rule identityKey;
    public Rule key;
    public Rule keyCollection;
    public Rule keyBlock;
    public Rule contextual;
    public Rule contextualCollection;
    public Rule contextualBlock;
    public Rule compilerItem;
    public Rule compilerBody;
    [Start]
    public Rule compilerDef;
    
    public SymphonyCompiler()
    {
        id = [
            [ IDENTITY ], 
            [ KEY ],
            [ CONTEXTUAL ]
        ];

        identityKey = [ [ IDENTITYKEY, EXPRESSION] ];

		key = [ [ KEY, id, EQUAL, EXPRESSION ] ];

        keyCollection = [ [ id, EQUAL, EXPRESSION ] ];
        keyCollection.Add([ id, EQUAL, EXPRESSION, keyCollection ]);

		keyBlock = [
            [ KEY, OPENBRACES, CLOSEBRACES ],
            [ KEY, OPENBRACES, keyCollection, CLOSEBRACES ]
        ];

		contextual = [ [ CONTEXTUAL, EXPRESSION ] ];

        contextualCollection = [ [ EXPRESSION ] ];
        contextualCollection.Add([ EXPRESSION, contextualCollection ]);

		contextualBlock = [
            [ CONTEXTUAL, OPENBRACES, CLOSEBRACES ],
            [ CONTEXTUAL, OPENBRACES, contextualCollection, CLOSEBRACES ]
        ];

        compilerItem = [ 
            [ identityKey ],
            [ key ],
            [ keyBlock ],
            [ contextual ],
            [ contextualBlock ]
        ];

        compilerBody = [ [ compilerItem ] ];
        compilerBody.Add([ compilerItem, compilerBody ]);

        compilerDef = [
            [ COMPILER, id, OPENBRACES, compilerBody, CLOSEBRACES ]
        ];
    }
}