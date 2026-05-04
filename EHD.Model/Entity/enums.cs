using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EHD.Model.Entity {

    public enum ActionType {
        Insert,
        Update,
        Delete
    }

    public enum LeftRight {
        None,
        Left,
        Right,
        Both
    }

    public enum YesNo {
        Unknown,
        Yes,
        No
    }

    public enum DominantHand {
        Unknown,
        Left,
        Right,
        Ambidextrous
    }

    public enum ChiropracticSOAPConditionChanges {
        Unknown,
        Improving,
        NoChange,
        Worsening,
        Aggravation,
        NewCondition
    }

    public enum PainKeys {
        Pain,
        Numbness,
        PinsAndNeedles,
        None
    }

    public enum Trend {
        Increasing,
        Static,
        Decreasing,
        None
    }

    public enum IrritabilityLevels {
        Low,
        Moderate,
        High,
        None
    }

    public enum ConsentForTreaments {
        Verbal,
        None
    }

    public enum Sex {
        Male,
        Female,
        Unknown
    }

    public enum HealthState {
        Unknown,
        Poor,
        Fair,
        Average,
        VeryGood,
        Excellent
    }

    public enum BodyTemperature {
        Unknown,
        Warmer,
        Cooler,
        Average
    }

    public enum SelfOther {
        Unknown,
        Self,
        Other
    }

    public enum Frequency {
        Unknow,
        Often,
        Sometimes,
        Never
    }

}
