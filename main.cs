using System;
using condor.classad;

namespace ClassAdClr{
  public class MainClass{
    public static void Main(){
      condor.classad.ClassAdParser req_parser = new ClassAdParser("other.memory >= 512 && other.CpuLoad < 2 && other.Type == \"Job\" && other.ResearchGroup == \"ACIS\"");
      condor.classad.ClassAdParser rank_parser = new ClassAdParser("other.memory + other.CpuLoad");

      Expr req_check = req_parser.parse();
      Expr rank_check = rank_parser.parse();
    

      RecordExpr test1 = new RecordExpr();
      RecordExpr test2 = new RecordExpr();
      RecordExpr test3 = new RecordExpr();
      
      test1.insertAttribute("rank", rank_check);
      test2.insertAttribute("rank", Constant.getInstance(1));
      test3.insertAttribute("rank", Constant.getInstance(1));
      test1.insertAttribute("requirements", req_check);

      ListExpr rs_group = new ListExpr();
      rs_group.add(Constant.getInstance("ACIS"));
      rs_group.add(Constant.getInstance("UF_ECE"));
      rs_group.add(Constant.getInstance("P2P"));

      test2.insertAttribute("ResearchGroup", rs_group);
      
      test2.insertAttribute("requirements", Constant.getInstance(true));
      test3.insertAttribute("requirements", Constant.getInstance(true));
      
      test2.insertAttribute("memory", Constant.getInstance(2048));
      test2.insertAttribute("type", Constant.getInstance("Job"));
      test3.insertAttribute("Type", Constant.getInstance("job"));
      test3.insertAttribute("memory", Constant.getInstance(1024));
      test2.insertAttribute("CpuLoad", Constant.getInstance(1));
      test3.insertAttribute("CpuLoad", Constant.getInstance(1));

      Console.WriteLine(test1);
      Console.WriteLine(test2);
      Console.WriteLine(test3);
      
      int[] match2 = ClassAd.match(test1, test2);
      int[] match3 = ClassAd.match(test1, test3);
      
      if (match2 != null){
          Console.WriteLine("match with 2 succeeded, ranks {0} and {1}\n", match2[0], match2[1]);
      }
      
      if (match3 != null){
          Console.WriteLine("match with 3 succeeded, ranks {0} and {1}\n", match3[0], match3[1]);
      }

    }
  }
}
