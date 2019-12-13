﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TINY_Compiler
{
    public class Node
    {
        public List<Node> Children = new List<Node>();
        
        public string Name; //Esm el-Node
        public Node(string N)
        {
            this.Name = N;
        }
    }
    public class Parser
    {
        int InputPointer = 0; // Boy2af 3la el-next token, works on TokenStream List
        List<Token> TokenStream;
        public  Node root;
        
        public Node StartParsing(List<Token> TokenStream)
        {
            this.TokenStream = TokenStream;
            root = new Node("Root Node");
            root.Children.Add(Program());
            return root;
        }
        Node Program() // Start Predection Rule
        {
            Node program = new Node("Program");
            /*
            program.Children.Add(Header());
            program.Children.Add(DeclSec());
            program.Children.Add(Block());
            program.Children.Add(match(Token_Class.Dot)); // match takes the expected Token
            MessageBox.Show("Success");
            */
            return program;
        }
        Node Main_Function()
        {
            Node place_holder = new Node("place_holder");
            return place_holder;
        }
        Node Function_Statement()
        {
            Node place_holder = new Node("place_holder");
            return place_holder;
        }
        Node Function_Declaration()
        {
            Node place_holder = new Node("place_holder");
            return place_holder;
        }
        Node FunctionName()
        {
            Node place_holder = new Node("place_holder");
            return place_holder;
        }
        Node Parameters()
        {
            Node place_holder = new Node("place_holder");
            return place_holder;
        }
        Node Parameters_()
        {
            Node place_holder = new Node("place_holder");
            return place_holder;
        }
        Node Parameter()
        {
            Node place_holder = new Node("place_holder");
            return place_holder;
        }
        Node Function_Body()
        {
            Node place_holder = new Node("place_holder");
            return place_holder;
        }

        Node Statements()
        {
            Node place_holder = new Node("place_holder");
            return place_holder;
        }
        Node Statement()
        {
            Node place_holder = new Node("place_holder");
            return place_holder;
        }
        Node Return_Statement()
        {
            Node place_holder = new Node("place_holder");
            return place_holder;
        }
        Node Function_Call()
        {
            Node place_holder = new Node("place_holder");
            return place_holder;
        }
        Node Identifiers()
        {
            Node place_holder = new Node("place_holder");
            return place_holder;
        }
        Node Identifiers_()
        {
            Node place_holder = new Node("place_holder");
            return place_holder;
        }
        Node Repeat_Statement()
        {
            Node place_holder = new Node("place_holder");
            return place_holder;
        }
        Node Condition_Statement()
        {
            Node place_holder = new Node("place_holder");
            return place_holder;
        }
        Node Condition_Statement_()
        {
            Node place_holder = new Node("place_holder");
            return place_holder;
        }
        Node Condition()
        {
            Node place_holder = new Node("place_holder");
            return place_holder;
        }
        Node Term()
        {
            Node place_holder = new Node("place_holder");
            return place_holder;
        }
        Node Write_Statement()
        {
            Node place_holder = new Node("place_holder");
            return place_holder;
        }
        Node Next()
        {
            Node place_holder = new Node("place_holder");
            return place_holder;
        }
        Node Read_Statement()
        {
            Node place_holder = new Node("place_holder");
            return place_holder;
        }
        Node Datatype()
        {
            Node place_holder = new Node("place_holder");
            return place_holder;
        }
        Node Declaration_Statement()
        {
            Node place_holder = new Node("place_holder");
            return place_holder;
        }

        Node Assignment_Statement()
        {
            //Assignment_Statement ➔ identifier:= Expression
            Node assignmentStatement = new Node("assignmentStatement");
            assignmentStatement.Children.Add(Match(Token_Class.Identifier));
            assignmentStatement.Children.Add(Match(Token_Class.Assignment));
            assignmentStatement.Children.Add(Expression());
            return assignmentStatement;
        }
        Node Expression()
        {
            //Expression ➔ String | Term | Equation
            Node expression = new Node("expression");
            if (CheckForNull(Token_Class.StringLiteral))
                expression.Children.Add(Match(Token_Class.StringLiteral));
            else if (CheckForNull(Token_Class.LBrace))
                expression.Children.Add(Equation());
            else
                expression.Children.Add(Term());
            return expression;
        }
        Node Equation()
        {
            //Equation ➔ TermEq Equation_
            Node equation = new Node("equation");
            equation.Children.Add(TermEq());
            equation.Children.Add(Equation_());
            return equation;
        }
        Node Equation_()
        {
            //Equation_ ➔ AddTerm Equation_ | ε
            Node equation_ = new Node("equation_");
            if (CheckForNull(Token_Class.PlusOp) || CheckForNull(Token_Class.MinusOp))
            {
                equation_.Children.Add(AddTerm());
                equation_.Children.Add(Equation_());
                return equation_;
            }
            return null;
        }
        Node AddTerm()
        {
            //AddTerm ➔ AddOperation TermEq
            Node addTerm = new Node("addTerm");
            addTerm.Children.Add(AddOperation());
            addTerm.Children.Add(TermEq());
            return addTerm;
        }
        Node TermEq()
        {
            //TermEq ➔ Factor TermEq_ 
            Node termEq = new Node("termEq");
            termEq.Children.Add(Factor());
            termEq.Children.Add(TermEq_());
            return termEq;
        }
        Node TermEq_()
        {
            //TermEq_ ➔ MulTerm TermEq_ | ε
            Node termEq_ = new Node("termEq_");
            if(CheckForNull(Token_Class.MultiplyOp) || CheckForNull(Token_Class.DivideOp))
            {
                termEq_.Children.Add(MulTerm());
                termEq_.Children.Add(TermEq_());
                return termEq_;
            }
            return null;
        }
        Node MulTerm()
        {
            //MulTerm ➔ MulOperation Factor
            Node mulTerm = new Node("mulTerm");
            mulTerm.Children.Add(MulOperation());
            mulTerm.Children.Add(Factor());
            return mulTerm;
        }
        Node Factor()
        {
            //Factor ➔ (Equation) | Term
            Node factor = new Node("factor");
            if (CheckForNull(Token_Class.LBrace))
            {
                factor.Children.Add(Match(Token_Class.LBrace));
                factor.Children.Add(Equation());
                factor.Children.Add(Match(Token_Class.RBrace));
            }
            else
            {
                factor.Children.Add(Term());
            }
            return factor;
        }
        Node AddOperation()
        {
            //AddOperation ➔ + | -
            Node addOperation = new Node("addOperation");
            if (CheckForNull(Token_Class.PlusOp))
                addOperation.Children.Add(Match(Token_Class.PlusOp));
            else
                addOperation.Children.Add(Match(Token_Class.MinusOp));
            return addOperation;
        }
        Node MulOperation()
        {
            //MulOperation ➔ * | /
            Node mulOperation = new Node("mulOperation");
            if (CheckForNull(Token_Class.MultiplyOp))
                mulOperation.Children.Add(Match(Token_Class.MultiplyOp));
            else
                mulOperation.Children.Add(Match(Token_Class.DivideOp));
            return mulOperation;
        }
        Node If_Statement()
        {
            //If_Statement ➔ if Condition_Statement then Statements ElseClause
            Node ifStatement = new Node("ifStatement");
            ifStatement.Children.Add(Match(Token_Class.If));
            ifStatement.Children.Add(Condition_Statement());
            ifStatement.Children.Add(Match(Token_Class.Then));
            ifStatement.Children.Add(Statements());
            ifStatement.Children.Add(ElseClause());
            return ifStatement;
        }
        Node ElseClause()
        {
            //ElseClause ➔ Else_If_Statment | Else_Statment | end
            Node elseClause = new Node("elseClause");
            if(CheckForNull(Token_Class.Elseif))
                elseClause.Children.Add(Else_If_Statment());
            else if(CheckForNull(Token_Class.Else))
                elseClause.Children.Add(Else_Statment()); 
            else
                elseClause.Children.Add(Match(Token_Class.End));
            return elseClause;
        }

        Node Else_If_Statment()
        {
            //Else_If_Statment ➔ elseif Condition_Statement then Statements ElseClause
            Node elseIfStatment = new Node("elseIfStatment");
            elseIfStatment.Children.Add(Match(Token_Class.Elseif));
            elseIfStatment.Children.Add(Condition_Statement());
            elseIfStatment.Children.Add(Match(Token_Class.Then));
            elseIfStatment.Children.Add(Statements());
            elseIfStatment.Children.Add(ElseClause());
            return elseIfStatment;
        }

        Node Else_Statment()
        {
            //Else_Statment ➔ else Statements end
            Node elseStatment = new Node("elseStatment");
            elseStatment.Children.Add(Match(Token_Class.Else));
            elseStatment.Children.Add(Statements());
            elseStatment.Children.Add(Match(Token_Class.End));
            return elseStatment;
        }

        public bool CheckForNull(Token_Class token)
        {
            if (TokenStream[InputPointer].token_type == token)
                return true;
            return false;
        }

        public Node Match(Token_Class ExpectedToken)
        {
            
            if (ExpectedToken == TokenStream[InputPointer].token_type)
            {
                InputPointer++;
                Node newNode = new Node(ExpectedToken.ToString());

                return newNode;
                    
            }

            else
            {
                Errors.Error_List.Add("Parsing Error: Expected " 
                    + ExpectedToken.ToString() + " and " + 
                    TokenStream[InputPointer].token_type.ToString() + 
                    "  found\r\n");
                InputPointer++;
                return null;
            }
        }

        public static TreeNode PrintParseTree(Node root)
        {
            TreeNode tree = new TreeNode("Parse Tree");
            TreeNode treeRoot = PrintTree(root);
            if (treeRoot != null)
                tree.Nodes.Add(treeRoot);
            return tree;
        }
        static TreeNode PrintTree(Node root)
        {
            if (root == null || root.Name == null)
                return null;
            TreeNode tree = new TreeNode(root.Name);
            if (root.Children.Count == 0)
                return tree;
            foreach (Node child in root.Children)
            {
                if (child == null)
                    continue;
                tree.Nodes.Add(PrintTree(child));
            }
            return tree;
        }
    }
}
