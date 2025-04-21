using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class CarInspection
{
    public int CarInspectionId { get; set; }

    public int? TeamId { get; set; }

    public int CarDocumentationId { get; set; }

    public byte Status { get; set; }

    public DateTime? ExpDate { get; set; }

    public virtual ICollection<AirbagCondition> AirbagConditions { get; set; } = new List<AirbagCondition>();

    public virtual ICollection<BodyCondition> BodyConditions { get; set; } = new List<BodyCondition>();

    public virtual ICollection<BrakeFluidLeakage> BrakeFluidLeakages { get; set; } = new List<BrakeFluidLeakage>();

    public virtual ICollection<BrakePad> BrakePads { get; set; } = new List<BrakePad>();

    public virtual ICollection<BrakePipe> BrakePipes { get; set; } = new List<BrakePipe>();

    public virtual ICollection<BrakeRotor> BrakeRotors { get; set; } = new List<BrakeRotor>();

    public virtual CarDocumentation CarDocumentation { get; set; } = null!;

    public virtual ICollection<CoolantLeakage> CoolantLeakages { get; set; } = new List<CoolantLeakage>();

    public virtual ICollection<CoolingCondition> CoolingConditions { get; set; } = new List<CoolingCondition>();

    public virtual ICollection<ElectricalCharging> ElectricalChargings { get; set; } = new List<ElectricalCharging>();

    public virtual ICollection<ElectricalWiring> ElectricalWirings { get; set; } = new List<ElectricalWiring>();

    public virtual ICollection<EngineAuthenticity> EngineAuthenticities { get; set; } = new List<EngineAuthenticity>();

    public virtual ICollection<EngineOilLeakage> EngineOilLeakages { get; set; } = new List<EngineOilLeakage>();

    public virtual ICollection<EngineSound> EngineSounds { get; set; } = new List<EngineSound>();

    public virtual ICollection<EngineTemperature> EngineTemperatures { get; set; } = new List<EngineTemperature>();

    public virtual ICollection<EngineVibration> EngineVibrations { get; set; } = new List<EngineVibration>();

    public virtual ICollection<FrontLight> FrontLights { get; set; } = new List<FrontLight>();

    public virtual ICollection<HeatingCondition> HeatingConditions { get; set; } = new List<HeatingCondition>();

    public virtual ICollection<PaintCondition> PaintConditions { get; set; } = new List<PaintCondition>();

    public virtual ICollection<RearLight> RearLights { get; set; } = new List<RearLight>();

    public virtual ICollection<Resume> Resumes { get; set; } = new List<Resume>();

    public virtual Team? Team { get; set; }

    public virtual ICollection<TireDepth> TireDepths { get; set; } = new List<TireDepth>();

    public virtual ICollection<TirePressure> TirePressures { get; set; } = new List<TirePressure>();
}
