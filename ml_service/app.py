from flask import Flask, jsonify
from services import forecasting, associations, segmentation

app = Flask(__name__)

@app.post("/train/forecast")
def train_forecast():
    rows = forecasting.run()
    return jsonify({"status": "ok", "rows_written": rows})

@app.post("/train/associations")
def train_associations():
    rows = associations.run()
    return jsonify({"status": "ok", "rows_written": rows})

@app.post("/train/segments")
def train_segments():
    rows = segmentation.run()
    return jsonify({"status": "ok", "rows_written": rows})

@app.get("/health")
def health():
    return jsonify({"status": "up"})

if __name__ == "__main__":
    from config import FLASK_PORT
    app.run(host="0.0.0.0", port=FLASK_PORT)
