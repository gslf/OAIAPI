namespace Promezio.OAIAPI.Capabilities.Assistants;

public enum AvailableToolTypes {
    CODE_INTERPRETER,
    RETRIEVAL,
    FUNCTION
}

public class ToolType {
    private AvailableToolTypes _type;
    public ToolType(AvailableToolTypes type) { _type = type; }
    public override string ToString() {
        switch (_type) {
            case AvailableToolTypes.CODE_INTERPRETER:
                return "code_interpreter";
            case AvailableToolTypes.RETRIEVAL:
                return "retrieval";
            case AvailableToolTypes.FUNCTION:
                return "function";
        }

        return "INVALID";
    }
}

public class Tool {
    public string? Type { get; set; }
    public ToolFunction? Function { get; set; }
}
