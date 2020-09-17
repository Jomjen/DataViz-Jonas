using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PersonalDataExample : MonoBehaviour
{
    public string dataCsvFileName = "";

    List<Person> _people = new List<Person>();
    int _ageMin, _ageMax;

    void Awake()
    {
        // Parse.
        string csvFilePath = Application.streamingAssetsPath + "/" + dataCsvFileName;
        string csvContent = File.ReadAllText(csvFilePath);
        Parse(csvContent);

        // filter.
        Filter();

        // Mine.
        Mine();

        // Represent.
        Represent();

        Debug.Log("_people.count: " + _people.Count);
    }
    void Parse(string csvText)
    {
        // Split by new lines in rows.
        string[] rowContents = csvText.Split('\n');
        // For each row.
        for (int r = 1; r < rowContents.Length; r++)
        {
            string rowContent = rowContents[r];
            string[] fieldContents = rowContent.Split(',');
            Person person = new Person(r);
            // For each field in this row.
            for (int f = 0; f < fieldContents.Length; f++)
            {
                string fieldContent = fieldContents[f];
                switch (f)
                {
                    case 0:
                        // First name.
                        person.firstName = fieldContent;
                        break;
                    case 1:
                        // Last name.
                        person.lastName = fieldContent;
                        break;
                    case 2:
                        // Age.
                        int age;
                        bool parseSucceeded = int.TryParse(fieldContent, out age);
                        if (parseSucceeded) person.age = age;
                        break;
                    case 3:
                        // Had covid
                        person.hadCovid = fieldContent.ToLower() == "yes";
                        break;
                }
            }
            // Parse covid relation level.
            Person.CovidRelationLevel covidRelationLevel = Person.CovidRelationLevel.None;
            if (fieldContents.Length > 6)
            {
                bool familyHadCovid = fieldContents[4].ToLower() == "yes";
                bool familyOrFriendsHadCovid = fieldContents[5].ToLower() == "yes";
                bool anyoneHadCovid = fieldContents[6].ToLower() == "yes";
                if (anyoneHadCovid) covidRelationLevel = Person.CovidRelationLevel.Anyone;
                else if (familyOrFriendsHadCovid) covidRelationLevel = Person.CovidRelationLevel.FamilyOrFriend;
                else if (familyOrFriendsHadCovid) covidRelationLevel = Person.CovidRelationLevel.Family;
            }
            person.covidRelationLevel = covidRelationLevel;

            //Add to person list.
            _people.Add(person);
        }
    }

    void Filter()
    {
        for( int p = _people.Count-1; p >= 0; p-- )
        {
            Person person = _people[p];

            if(person.age < 18 || person.age > 127) { //If too young or (||) too old. 
                _people.RemoveAt(p);
                Debug.Log(person.firstName + " is " + "invalid");
            }
            
        }
    }
    
    void Mine()
    {
        _ageMin = int.MaxValue;
        _ageMax = int.MinValue;
        foreach( Person person in _people)
        {
            if (person.age > _ageMax) _ageMax = person.age;
            else if (person.age < _ageMin) _ageMin = person.age;
        }
        Debug.Log( "Min: " + _ageMin + ", Max: " + _ageMax);
    }
    

    void Represent()
    {
        for( int p = 0; p < _people.Count; p++)
        {
            Person person = _people[p];

            float x = p;
            float height = Mathf.InverseLerp( _ageMin, _ageMax, person.age) * 10;
            float y = height * 0.5f;
            float width = 0.9f;

            GameObject mainObject = new GameObject(person.id + " " + person.firstName );
            mainObject.transform.SetParent(transform);
            mainObject.transform.localPosition = new Vector3(x, y, 0);

            GameObject barObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            barObject.transform.SetParent(mainObject.transform);
            barObject.transform.localPosition = Vector3.zero;
            barObject.transform.localScale = new Vector3(width, height, 1);
        }
    }

}