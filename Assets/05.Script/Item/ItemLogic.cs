using System.Text;
public static class ItemLogic
{
    public static bool IsResource(int key)
    {
        // 1000 아래는 기타자원
        return key < 1000;
    }
    public static bool IsEquip(int key)
    {
        // 장비품
        return key >= 1000 && key < 10000;
    }
    public static bool IsConsumable(int key)
    {
        // 소모품
        return key >= 10000;
    }
    public static bool IsSamePart(int firKey, int secKey)
    {
        if (IsEquip(firKey) && IsEquip(secKey))
        {
            int typeFir = firKey / 1000;
            int typeSec = secKey / 1000;
            return typeFir == typeSec;
        }
        else
            return false;
    }
    public static string StatText(Item item)
    {
        StringBuilder sb = new StringBuilder();

        if (item.health != 0) sb.AppendLine($"체력: {item.health}");
        if (item.attack != 0) sb.AppendLine($"공격력: {item.attack}");
        if (item.defense != 0) sb.AppendLine($"방어력: {item.defense}");
        if (item.critical != 0) sb.AppendLine($"크리티컬: {item.critical}");

        return sb.ToString().TrimEnd(); // 마지막 개행문자 제거
    }
}
