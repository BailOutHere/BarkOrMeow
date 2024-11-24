using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace ChalkSave.Mods;

public class InjectPlayer : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        // [PlayerData.cosmetics_equipped.species]
        var barkFuncWaiter = new MultiTokenWaiter([
            t => t.Type is TokenType.BracketOpen,
            t => t is IdentifierToken{ Name: "PlayerData" },
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken{ Name: "cosmetics_equipped" },
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken{ Name: "species" },
            t => t.Type is TokenType.BracketClose,
            t => t.Type is TokenType.Newline
        ]);


        foreach (var token in tokens)
        {
            
            if (barkFuncWaiter.Check(token))
            {
                yield return token;
                yield return new Token(TokenType.Newline, 1);

                // if Input.is_action_pressed("secondary_action"):
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("Input");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("is_action_pressed");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("secondary_action"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                
                //if PlayerData.cosmetics_equipped.species == "species_cat":'
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("cosmetics_equipped");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("species");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("species_cat"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                
                // bark_id = ["bark_dog", "growl_dog", "whine_dog"]
                
                yield return new IdentifierToken("bark_id");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("bark_dog"));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("growl_dog"));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("whine_dog"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Newline, 2);
                
                //else: bark_id = ["bark_cat", "growl_cat", "whine_cat"]
                yield return new Token(TokenType.CfElse);
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("bark_id");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("bark_cat"));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("growl_cat"));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("whine_cat"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Newline, 1);
                
                
            }
            else yield return token;
        }
    }
}

