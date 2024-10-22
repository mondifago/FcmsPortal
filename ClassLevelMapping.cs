using FcmsPortal.Enums;

namespace FcmsPortal;

public class ClassLevelMapping
{
    public Dictionary<EducationLevel, List<ClassLevel>> GetClassLevelsByEducationLevel()
    {
        return new Dictionary<EducationLevel, List<ClassLevel>>
        {
            { EducationLevel.Kindergarten, new List<ClassLevel> { ClassLevel.KG_Daycare, ClassLevel.KG_PlayGroup, ClassLevel.KG_PreNursery, ClassLevel.KG_Nursery } },
            { EducationLevel.Primary, new List<ClassLevel> { ClassLevel.PRI_1, ClassLevel.PRI_2, ClassLevel.PRI_3, ClassLevel.PRI_4, ClassLevel.PRI_5, ClassLevel.PRI_6 } },
            { EducationLevel.JuniorCollege, new List<ClassLevel> { ClassLevel.JC_1, ClassLevel.JC_2, ClassLevel.JC_3 } },
            { EducationLevel.SeniorCollege, new List<ClassLevel> { ClassLevel.SC_1, ClassLevel.SC_2, ClassLevel.SC_3 } }
        };
    }
}




